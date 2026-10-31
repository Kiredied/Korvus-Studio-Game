using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class IndividualDamage : MonoBehaviour
{
    [SerializeField, Min(1)] private int requiredHammerHits = 5;
    [SerializeField, Min(0.1f)] private float plateTimeoutDuration = 10f;
    [SerializeField, Min(0.1f)] private float plateFadeDuration = 3f;
    [SerializeField] private Transform plateEjectTransform;
    [SerializeField] private GameObject plateTeleportTarget;
    [SerializeField] private XRSocketInteractor socket;

    // 50 FixedUpdate steps = 1 second at 50fps
    private const int FixedStepsPerSecond = 50;

    // 5 FixedUpdate steps = 0.1 seconds at 50fps, gives the socket time to deactivate before the teleport
    private const int SocketDeactivateDelaySteps = 5;

    // How long the socket stays off after a timeout eject, so the plate doesn't snap back in
    private const float SocketEjectReenableDelay = 2f;

    // When the fade-out reaches this alpha (85% transparent), the plate snaps to fully invisible
    private const float SnapInvisibleAlpha = 0.01f;

    public bool objectdamaged;

    private bool plateInTrigger;
    private GameObject activePlate;
    private int hammerHits;
    private float plateSocketTime;
    private bool isRepairing;
    private GameObject damageParticleObject;

    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");

    private static Color GetMaterialColor(Material mat)
    {
        if (mat.HasProperty(BaseColorId)) return mat.GetColor(BaseColorId);
        if (mat.HasProperty(ColorId)) return mat.GetColor(ColorId);
        return Color.white;
    }

    private static void SetMaterialColor(Material mat, Color color)
    {
        if (mat.HasProperty(BaseColorId)) mat.SetColor(BaseColorId, color);
        if (mat.HasProperty(ColorId)) mat.SetColor(ColorId, color);
    }

    private void Start()
    {
        if (socket == null)
        {
            Debug.LogWarning("IndividualDamage: no socket assigned on " + name);
        }

        ParticleSystem damageParticles = GetComponentInChildren<ParticleSystem>(true);
        if (damageParticles != null)
        {
            damageParticleObject = damageParticles.gameObject;
        }
        else
        {
            Debug.LogWarning("IndividualDamage: no particle system found under " + name);
        }
    }

    public void SocketEnter(SelectEnterEventArgs args)
    {
        if (args == null || args.interactableObject == null)
        {
            return;
        }

        IXRSelectInteractable interactable = args.interactableObject;
        activePlate = interactable.transform.gameObject;

        plateInTrigger = true;
        hammerHits = 0;
        plateSocketTime = Time.time;
        isRepairing = false;

        // Hide the damage particles while a plate is socketed
        if (damageParticleObject != null)
        {
            damageParticleObject.SetActive(false);
        }

        Debug.Log("Repair plate socketed: " + activePlate.name);
    }

    public void SocketExit(SelectExitEventArgs args)
    {
        if (args == null || args.interactableObject == null)
        {
            return;
        }

        IXRSelectInteractable interactable = args.interactableObject;

        if (interactable.transform.gameObject != activePlate)
        {
            return;
        }

        // Don't null activePlate here if repair is in progress
        if (!isRepairing)
        {
            activePlate = null;
        }

        plateInTrigger = false;
        hammerHits = 0;

        // Show the damage particles again if this location is still damaged
        if (objectdamaged && damageParticleObject != null)
        {
            damageParticleObject.SetActive(true);
        }

        Debug.Log("Repair plate removed.");
    }

    private void FixedUpdate()
    {
        // Check if plate has been in socket too long without hammer hits
        if (plateInTrigger && !isRepairing && Time.time - plateSocketTime > plateTimeoutDuration)
        {
            Debug.Log("Plate timeout - plate fell off");
            EjectPlate();
        }
    }

    public void OnHammerTrigger(Collider other)
    {
        if (other.gameObject.name == "HammerTriggerCollider")
        {
            RegisterHammerHit();
        }
    }

    private void RegisterHammerHit()
    {
        if (!objectdamaged || !plateInTrigger)
        {
            return;
        }

        hammerHits++;

        // Reset the timeout so the plate only falls off after a pause in hits
        plateSocketTime = Time.time;

        Debug.Log(
            "Hammer hit " + hammerHits + "/" + requiredHammerHits
        );

        if (hammerHits >= requiredHammerHits)
        {
            Repair();
        }
    }

    private void EjectPlate()
    {
        if (socket != null && activePlate != null)
        {
            // Move plate to eject position
            if (plateEjectTransform != null)
            {
                activePlate.transform.position = plateEjectTransform.position;
                activePlate.transform.rotation = plateEjectTransform.rotation;
            }

            // Force deselect from socket
            socket.interactionManager.SelectExit(socket, activePlate.GetComponent<IXRSelectInteractable>());

            // Turn the socket off so the plate doesn't snap straight back in, then re-enable it after a delay
            socket.gameObject.SetActive(false);
            StartCoroutine(ReenableSocketAfterDelay(socket.gameObject, SocketEjectReenableDelay));
        }

        activePlate = null;
        plateInTrigger = false;
        hammerHits = 0;
    }

    private IEnumerator ReenableSocketAfterDelay(GameObject socketObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (socketObject != null)
        {
            socketObject.SetActive(true);
        }
    }

    private void Repair()
    {
        isRepairing = true;

        ParticleSystem particleSystem =
            GetComponentInChildren<ParticleSystem>(true);

        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        objectdamaged = false;
        hammerHits = 0;
        plateInTrigger = false;

        ZeppelinDamage.RegisterRepair(this);

        Debug.Log("Damage repaired: " + name);

        // Start fade-out coroutine with the plate reference
        if (activePlate != null)
        {
            StartCoroutine(FadePlateAndReset(activePlate));
        }

        activePlate = null;
    }

    private IEnumerator FadePlateAndReset(GameObject plate)
    {
        if (plate == null)
        {
            yield break;
        }

        // Get all renderers on the plate and its children
        Renderer[] renderers = plate.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            Debug.LogWarning("FadePlateAndReset: no renderer found on " + plate.name);
            yield break;
        }

        // Store original colors, forcing alpha to 1 so the plate always starts fully opaque
        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            Color c = GetMaterialColor(renderers[i].material);
            c.a = 1f;
            originalColors[i] = c;
            SetMaterialColor(renderers[i].material, c);
        }

        Debug.Log("Starting fade-out for plate: " + plate.name);

        // Fade out until the alpha reaches the snap threshold. The loop only exits through that check,
        // so nothing after it runs until the plate has reached SnapInvisibleAlpha.
        float elapsedTime = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / plateFadeDuration);

            if (alpha <= SnapInvisibleAlpha)
            {
                break;
            }

            for (int i = 0; i < renderers.Length; i++)
            {
                Color fadeColor = originalColors[i];
                fadeColor.a = alpha;
                SetMaterialColor(renderers[i].material, fadeColor);
            }

            yield return null;
        }

        // Snap to fully invisible at the threshold
        for (int i = 0; i < renderers.Length; i++)
        {
            Color fadeColor = originalColors[i];
            fadeColor.a = 0f;
            SetMaterialColor(renderers[i].material, fadeColor);
        }

        if (plateTeleportTarget == null)
        {
            Debug.LogWarning("FadePlateAndReset: no teleport target assigned on " + name);
            yield break;
        }

        // Deactivate the socket the plate is sitting in, then wait so it takes effect before the teleport
        GameObject socketObject = null;
        if (socket != null)
        {
            socketObject = socket.gameObject;
            socketObject.SetActive(false);
        }

        for (int i = 0; i < SocketDeactivateDelaySteps; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Plate faded. Teleporting to: " + plateTeleportTarget.name);

        // Disable physics before moving
        Rigidbody rb = plate.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport plate to the target transform
        plate.transform.position = plateTeleportTarget.transform.position;
        plate.transform.rotation = plateTeleportTarget.transform.rotation;

        // Re-enable physics
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Wait 1 second while hidden (50 FixedUpdate steps)
        for (int i = 0; i < FixedStepsPerSecond; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        // Fade in over 1 second (50 FixedUpdate steps)
        for (int i = 1; i <= FixedStepsPerSecond; i++)
        {
            float t = (float)i / FixedStepsPerSecond;
            float alpha = Mathf.SmoothStep(0f, 1f, t);

            for (int j = 0; j < renderers.Length; j++)
            {
                Color fadeColor = originalColors[j];
                fadeColor.a = alpha;
                SetMaterialColor(renderers[j].material, fadeColor);
            }

            yield return new WaitForFixedUpdate();
        }

        // Fully opaque
        for (int i = 0; i < renderers.Length; i++)
        {
            SetMaterialColor(renderers[i].material, originalColors[i]);
        }

        // Reactivate the socket now that the plate is fully visible
        if (socketObject != null)
        {
            socketObject.SetActive(true);
        }

        Debug.Log("Plate reset and visible at: " + plateTeleportTarget.name);
    }
}