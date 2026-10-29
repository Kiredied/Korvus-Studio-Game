using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DangerZone2D : MonoBehaviour
{
    Image Image;
    [SerializeField] ZeppelinMapMovement ZeppelinMapMovement;

    [SerializeField] GameObject DarknessGame;
    bool revertlog = false;


    private int DeathTimer;
    static public bool DeathActivated = false;
    static public bool ConsiousnessRemover = false;
    public static bool DarknessRemover = false;

    private bool visible = false;
    static public bool CanDoResearch = false;
    static public bool PreviousDangerZone = false;
    static public bool DangerZone = false;
    static public bool DeadZone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeathTimer = 0;
        Image = DarknessGame.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Physics update, so 50 FPS. 50 DeathTimer = 1 second. 
        if (DeadZone)
        {
            DeathTimer += 1;
            // If DeathTimer reaches 150 (3 secs), player dies.
            if (DeathTimer >= 150)
            {
                if (!DeathActivated) ZeppelinMapMovement.Death();
                //DeathActivated = true;
                /*DeathFlickering.StartMinimapFlicker();
                DeathFlickering.StartMapFlicker();
                DeathFlickering.StartLightsFlicker();*/
            }
        }

        if (DarknessRemover)
        {
            if (!revertlog) Debug.Log("Restoring consciousness. (Post-Processing)");{ revertlog = true; }
            if (DarknessGame != null)
            {
                if (Image != null)
                {
                    Image.color = new Color(0, 0, 0, ((Image.color.a * 255f) * 30 + 0f) / 31f / 255f);
                }
            }
        }
    }
    static public void StartFlickering(DeathFlickering instance)
    {
        instance.StartCoroutine(instance.MinimapFlicker());
        instance.StartCoroutine(instance.MapFlicker());
        instance.StartCoroutine(instance.LightsFlicker());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("CurrentDangerZone"))
        {
            
            if (other.CompareTag("2DPlayer"))
            {
                Debug.Log("CurrentDangerZone entered (yes current, not previous)");
                if (visible == false)
                {
                    visible = true;
                    GetComponent<MeshRenderer>().enabled = true;
                }
                // enable research
                CanDoResearch = true;
                DangerZone = true;
                // notify player that they entered current danger zone

            }
        }

        if (this.gameObject.CompareTag("PreviousDangerZone"))
        {
            Debug.Log("PreviousDangerZone entered");
            // notify player that they entered previous danger zone
            PreviousDangerZone = true;
        }


        if (this.gameObject.CompareTag("DangerWalls"))
        {
            Debug.Log("DangerWalls entered");
            // give danger warning
            DangerZone = true;
        }
        if (this.gameObject.CompareTag("DeathWalls"))
        {
            Debug.Log("DeathWalls entered");
            // give a very bad warning (like a jumpscare)

            ZeppelinMapMovement.DeathRotation = ZeppelinMapMovement.RigidBody2DZeppelin.rotation;
            DeadZone = true;
           /* DeathFlickering.StartMinimapFlicker();
            DeathFlickering.StartMapFlicker();
            DeathFlickering.StartLightsFlicker();*/
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.MinimapFlicker());
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.MapFlicker());
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.LightsFlicker());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.CompareTag("CurrentDangerZone"))
        {
            Debug.Log("CurrentDangerZone exited (yes current, not previous)");
            if (other.CompareTag("2DPlayer"))
            {
                CanDoResearch = false;
                DangerZone = false;
                // disable research
            }
        }

        if (this.gameObject.CompareTag("PreviousDangerZone"))
        {
            Debug.Log("PreviousDangerZone exited");
            // do nothing
            PreviousDangerZone = false;
        }

        
        if (this.gameObject.CompareTag("DangerWalls"))
        {
            Debug.Log("DangerWalls exited");
            // remove danger warning
            DangerZone = false;
        }
        if (this.gameObject.CompareTag("DeathWalls"))
        {
            Debug.Log("DeathWalls exited");
            // reset death timer and remove fatal warning
            DeadZone = false;
            DeathTimer = 0;
            DeathFlickering.instance.StopAllFlickering();
        }
    }

}
