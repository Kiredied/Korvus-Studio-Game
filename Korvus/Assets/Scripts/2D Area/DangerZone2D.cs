using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DangerZone2D : MonoBehaviour
{
    Color darknessColor;
    [SerializeField] ZeppelinMapMovement ZeppelinMapMovement;

    [SerializeField] GameObject DarknessGame;
    [SerializeField] Material DarknessMaterialTransparent;
    [SerializeField] Material DarknessMaterialOpaque;
    bool revertlog = false;


    private int DeathTimer;
    static public bool DeathActivated = false;
    static public bool ConsiousnessRemover = false;
    static public bool MountainDeath = false;
    public static bool DarknessRemover = false;
    public static bool DarknessAdder = false;

    private bool visible = false;
    static public bool CanDoResearch = false;
    static public bool PreviousDangerZone = false;
    static public bool DangerWall = false;
    static public bool DangerZone = false;
    static public bool DeadZone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeathTimer = 0;
        darknessColor = new Color(0, 0, 0, 1);
        DarknessGame.GetComponent<MeshRenderer>().material = DarknessMaterialOpaque;
        DarknessGame.GetComponent<MeshRenderer>().material.color = darknessColor;
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
            var count = 0;
            if (!revertlog) Debug.Log("Restoring consciousness. (Post-Processing)");{ revertlog = true; }
            if (DarknessGame != null)
            {
                count += 1;
                DarknessGame.GetComponent<MeshRenderer>().material = DarknessMaterialTransparent;
                darknessColor = new Color(0, 0, 0, ((darknessColor.a * 255f) * 30 + 0f) / 31f / 255f);
                DarknessGame.GetComponent<MeshRenderer>().material.color = darknessColor;
                if (count > 50)
                {
                    DarknessRemover = false;
                }
            }
        }
        if (DarknessAdder)
        {
            var count = 0;
            if (!revertlog) Debug.Log("Restoring consciousness. (Post-Processing)");
            { revertlog = true; }
            if (DarknessGame != null)
            {
                count += 1;
                DarknessGame.GetComponent<MeshRenderer>().material = DarknessMaterialTransparent;
                darknessColor = new Color(0, 0, 0, ((darknessColor.a * 255f) * 30 + 255f) / 31f / 255f);
                DarknessGame.GetComponent<MeshRenderer>().material.color = darknessColor;
                if (count > 50)
                {
                    DarknessRemover = false;
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

                /*ZeppelinDamage.minDamageInterval = 15f;
                ZeppelinDamage.maxDamageInterval = 30f;*/
            }
        }

        if (this.gameObject.CompareTag("PreviousDangerZone"))
        {
            Debug.Log("PreviousDangerZone entered");
            // notify player that they entered previous danger zone
            PreviousDangerZone = true;
            /*ZeppelinDamage.minDamageInterval = 20f;
            ZeppelinDamage.maxDamageInterval = 40f;*/
        }


        if (this.gameObject.CompareTag("DangerWalls"))
        {
            Debug.Log("DangerWalls entered");
            // give danger warning
            DangerWall = true;
            DangerZone = true;
            /*ZeppelinDamage.minDamageInterval = 10f;
            ZeppelinDamage.maxDamageInterval = 15f;*/
        }
        if (this.gameObject.CompareTag("DeathWalls"))
        {
            Debug.Log("DeathWalls entered");
            // give a very bad warning (like a jumpscare)

            ZeppelinMapMovement.DeathRotation = ZeppelinMapMovement.RigidBody2DZeppelin.rotation;
            DeadZone = true;
            DarknessAdder = true;
            /* DeathFlickering.StartMinimapFlicker();
             DeathFlickering.StartMapFlicker();
             DeathFlickering.StartLightsFlicker();*/
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.MinimapFlicker());
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.MapFlicker());
            DeathFlickering.instance.StartCoroutine(DeathFlickering.instance.LightsFlicker());
            /*ZeppelinDamage.minDamageInterval = 2f;
            ZeppelinDamage.maxDamageInterval = 5f;*/
        }
        if (this.gameObject.CompareTag("Mountain"))
        {
            if (other.gameObject.tag == "2DPlayer")
            {
                //SceneManager.LoadScene("DeathScene");
                MountainDeath = true;
                DarknessAdder = true;
                
            }
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