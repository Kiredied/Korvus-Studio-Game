using UnityEngine;
using UnityEngine.InputSystem;

public class DangerZone2D : MonoBehaviour
{

    private int DeathTimer;
    static public bool DeathActivated = false;
    private bool visible = false;
    static public bool CanDoResearch = false;
    static public bool PreviousDangerZone = false;
    static public bool DangerZone = false;
    static public bool DeadZone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeathTimer = 0;
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
                DeathActivated = true;
            }
        }
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

        }
    }
}
