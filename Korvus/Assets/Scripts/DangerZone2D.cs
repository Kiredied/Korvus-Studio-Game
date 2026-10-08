using UnityEngine;
using UnityEngine.InputSystem;

public class DangerZone2D : MonoBehaviour
{


    private bool visible = false;
    static public bool CanDoResearch = false;
    static public bool PreviousDangerZone = false;
    static public bool DangerZone = false;
    static public bool DeadZone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // kill player

            DeadZone = false;

        }
    }
}
