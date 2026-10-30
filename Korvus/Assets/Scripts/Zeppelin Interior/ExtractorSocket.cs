using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ExtractorSocket : MonoBehaviour
{
    static public bool canister1insocket = false;
    static public bool canister2insocket = false;
    static public bool canister3insocket = false;
    
    bool canister1insocketcheck = false;
    bool canister2insocketcheck = false;
    bool canister3insocketcheck = false;

    [SerializeField] GameObject Canister1;
    [SerializeField] GameObject Canister2;
    [SerializeField] GameObject Canister3;

    [SerializeField] GameObject OutOfBoundsSocket;
    [SerializeField] GameObject OutOfBoundsSocketTransform;

    static public bool anycanisterinsocket = false;


    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Canister1.transform.position == OutOfBoundsSocketTransform.transform.position)
        {
            OutOfBoundsSocket.SetActive(false);
        }
        else
        {
            OutOfBoundsSocket.SetActive(true);
        }

        if (Canister2.transform.position == OutOfBoundsSocketTransform.transform.position)
        {
            OutOfBoundsSocket.SetActive(false);
        }
        else
        {
            OutOfBoundsSocket.SetActive(true);
        }

        if (Canister3.transform.position == OutOfBoundsSocketTransform.transform.position)
        {
            OutOfBoundsSocket.SetActive(false);
        }
        else
        {
            OutOfBoundsSocket.SetActive(true);
        }
        

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        
        if (other.transform.parent.CompareTag("Canister"))
        {
            Debug.Log("Canister collided");
            if (other.transform.parent.name == "CanisterObject1")
            {
                Debug.Log(other.transform.parent.name + " is now in progress.");
                canister1insocket = true;
                if (!ExtractorCode.RealProgressBar3D1Done || canister1insocketcheck)
                {
                    other.transform.parent.GetComponentInChildren<Collider>().enabled = false;
                }

            } else if (other.transform.parent.name == "CanisterObject2")
            {
                Debug.Log(other.transform.parent.name + " is now in progress.");
                canister2insocket = true;
                if (!ExtractorCode.RealProgressBar3D2Done || canister2insocketcheck)
                {
                    other.transform.parent.GetComponentInChildren<Collider>().enabled = false;
                }

            } else if (other.transform.parent.name == "CanisterObject3")
            {
                Debug.Log(other.transform.parent.name + " is now in progress.");
                canister3insocket = true;
                if (!ExtractorCode.RealProgressBar3D3Done || canister3insocketcheck)
                {
                    other.transform.parent.GetComponentInChildren<Collider>().enabled = false;
                }

            } else { Debug.LogError("Something is tagged wrong on " + other.transform.parent.name + " or "  + other.name + " or " + this.name +  ", or the code broke. (Enter)"); }
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.transform.parent.CompareTag("Canister"))
        {
            Debug.Log("Canister Left collision");
            if (other.transform.parent.name == "CanisterObject1")
            {
                Debug.Log(other.transform.parent.name + " is now done.");
                canister1insocket = false;
            } else if (other.transform.parent.name == "CanisterObject2")
            {
                Debug.Log(other.transform.parent.name + " is now done.");
                canister2insocket = false;
            } else if (other.transform.parent.name == "CanisterObject3")
            {
                Debug.Log(other.transform.parent.name + " is now done.");
                canister3insocket = false;
            } else { Debug.LogError("Something is tagged wrong on " + other.name + " or " + this.name +  ", or the code broke. (Exit)"); }

        }
        
    }*/

    public void enterSocket(SelectEnterEventArgs args)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject;
        GameObject obj = interactable.transform.gameObject;

        Debug.Log("Socketed: " + obj.name);

        if (obj.CompareTag("Canister"))
        {
            Debug.Log("Canister socketed");
            anycanisterinsocket = true;
            if (obj.name == "CanisterObject1")
            {
                Debug.Log(obj.name + " is now in progress.");
                canister1insocket = true;
                if (!ExtractorCode.RealProgressBar3D1Done || canister1insocketcheck)
                {
                    obj.GetComponentInChildren<Collider>().enabled = false;
                }
            }
            else if (obj.name == "CanisterObject2")
            {
                Debug.Log(obj.name + " is now in progress.");
                canister2insocket = true;
                if (!ExtractorCode.RealProgressBar3D2Done || canister2insocketcheck)
                {
                    obj.GetComponentInChildren<Collider>().enabled = false;
                }
            }
            else if (obj.name == "CanisterObject3")
            {
                Debug.Log(obj.name + " is now in progress.");
                canister3insocket = true;
                if (!ExtractorCode.RealProgressBar3D3Done || canister3insocketcheck)
                {
                    obj.GetComponentInChildren<Collider>().enabled = false;
                }
            }
            else 
            { 
                Debug.LogError("Something is tagged wrong on " + obj.name + " or " + this.name + ", or the code broke. (Enter)"); 
            }
        }
    }

    public void exitSocket(SelectExitEventArgs args)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable = args.interactableObject;
        GameObject obj = interactable.transform.gameObject;

        if (obj.CompareTag("Canister"))
        {
            anycanisterinsocket = false;
            Debug.Log("Canister Left socket");
            if (obj.name == "CanisterObject1")
            {
                Debug.Log(obj.name + " is now done.");
                canister1insocket = false;
            }
            else if (obj.name == "CanisterObject2")
            {
                Debug.Log(obj.name + " is now done.");
                canister2insocket = false;
            }
            else if (obj.name == "CanisterObject3")
            {
                Debug.Log(obj.name + " is now done.");
                canister3insocket = false;
            }
            else 
            { 
                Debug.LogError("Something is tagged wrong on " + obj.name + " or " + this.name + ", or the code broke. (Exit)"); 
            }

            ExtractorCode.resetTempProgressBar2DValue = true;
        }
    }

   
}
