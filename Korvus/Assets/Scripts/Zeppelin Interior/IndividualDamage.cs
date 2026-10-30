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
    [SerializeField, Min(0.1f)] private float plateFadeDuration = 10f;
    [SerializeField] private Transform plateEjectTransform;

    public bool objectdamaged;

    private bool plateInTrigger;
    private GameObject activePlate;
    private int hammerHits;
    private float plateSocketTime;
    private bool isRepairing;
    private Vector3 plateStartPosition;
    private Quaternion plateStartRotation;
    private bool platePositionCaptured;
    private XRSocketInteractor socket;

    private void Start()
    {
        socket = GetComponentInParent<XRSocketInteractor>();
    }

    public void SocketEnter(SelectEnterEventArgs args)
    {
        if (args == null || args.interactableObject == null)
        {
            return;
        }

        IXRSelectInteractable interactable = args.interactableObject;
        activePlate = interactable.transform.gameObject;

        // Capture start position on first socket
        if (!platePositionCaptured)
        {
            plateStartPosition = activePlate.transform.position;
            plateStartRotation = activePlate.transform.rotation;
            platePositionCaptured = true;

            Debug.Log("Plate start position captured: " + plateStartPosition);
        }

        plateInTrigger = true;
        hammerHits = 0;
        plateSocketTime = Time.time;
        isRepairing = false;

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

    private void OnTriggerEnter(Collider other)
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
        }

        activePlate = null;
        plateInTrigger = false;
        hammerHits = 0;
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

        if (activePlate != null)
        {
            Collider[] plateColliders =
                activePlate.GetComponentsInChildren<Collider>();

            foreach (Collider plateCollider in plateColliders)
            {
                plateCollider.enabled = false;
            }
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

    private System.Collections.IEnumerator FadePlateAndReset(GameObject plate)
    {
        if (plate == null)
        {
            yield break;
        }

        // Force eject from socket FIRST
        if (socket != null)
        {
            IXRSelectInteractable selectInteractable = plate.GetComponent<IXRSelectInteractable>();
            if (selectInteractable != null)
            {
                socket.interactionManager.SelectExit(socket, selectInteractable);
                Debug.Log("Plate ejected from socket");
            }
        }

        // Get all renderers on the plate and its children
        Renderer[] renderers = plate.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning("FadePlateAndReset: no renderer found on " + plate.name);
            yield break;
        }

        // Store original colors for all materials
        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }

        float elapsedTime = 0f;

        Debug.Log("Starting fade-out for plate: " + plate.name);

        // Fade out
        while (elapsedTime < plateFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / plateFadeDuration);

            for (int i = 0; i < renderers.Length; i++)
            {
                Color fadeColor = originalColors[i];
                fadeColor.a = alpha;
                renderers[i].material.color = fadeColor;
            }

            yield return null;
        }

        // Fully transparent
        for (int i = 0; i < renderers.Length; i++)
        {
            Color fadeColor = originalColors[i];
            fadeColor.a = 0f;
            renderers[i].material.color = fadeColor;
        }

        Debug.Log("Plate faded. Teleporting to: " + plateStartPosition);

        // Disable physics before moving
        Rigidbody rb = plate.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport plate
        plate.transform.position = plateStartPosition;
        plate.transform.rotation = plateStartRotation;

        // Re-enable physics
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Fade in
        elapsedTime = 0f;
        while (elapsedTime < plateFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / plateFadeDuration);

            for (int i = 0; i < renderers.Length; i++)
            {
                Color fadeColor = originalColors[i];
                fadeColor.a = alpha;
                renderers[i].material.color = fadeColor;
            }

            yield return null;
        }

        // Fully opaque
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }

        Debug.Log("Plate reset to start position: " + plateStartPosition);
    }
}
