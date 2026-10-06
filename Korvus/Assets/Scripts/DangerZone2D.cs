using UnityEngine;
using UnityEngine.InputSystem;

public class DangerZone2D : MonoBehaviour
{


    static public bool visible = false;
    static public bool CanDoResearch = false;
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
            Debug.Log("CurrentDangerZone entered (yes current, not previous)");
            if (other.CompareTag("2DPlayer"))
            {
                if (visible == false)
                {
                    visible = true;
                    GetComponent<MeshRenderer>().enabled = true;
                }
                CanDoResearch = true;
                // enable research
                // notify player that they entered current danger zone
            }
        }

        if (this.gameObject.CompareTag("PreviousDangerZone"))
        {
            Debug.Log("PreviousDangerZone entered");
            // notify player that they entered previous danger zone
        }


        if (this.gameObject.CompareTag("DangerWalls"))
        {
            Debug.Log("DangerWalls entered");
            // give danger warning
        }
        if (this.gameObject.CompareTag("DeathWalls"))
        {
            Debug.Log("DeathWalls entered");
            // give a very bad warning (like a jumpscare)
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
                // disable research
            }
        }

        if (this.gameObject.CompareTag("PreviousDangerZone"))
        {
            Debug.Log("PreviousDangerZone exited");
            // do nothing
        }

        
        if (this.gameObject.CompareTag("DangerWalls"))
        {
            Debug.Log("DangerWalls exited");
            // remove danger warning
        }
        if (this.gameObject.CompareTag("DeathWalls"))
        {
            Debug.Log("DeathWalls exited");
            // kill player
        }
    }
}
