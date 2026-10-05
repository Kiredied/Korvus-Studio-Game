using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class buttontest : MonoBehaviour
{

    [SerializeField] XRSimpleInteractable interactable;
    public UnityEvent onPressed;


    bool buttonenabled = false;
    int ButtonCooldown = 60;
    public Material ButtonEnabled;
    public Material ButtonDisabled;

    void OnEnable() => interactable.selectEntered.AddListener(OnSelect);
    void OnDisable() => interactable.selectEntered.RemoveListener(OnSelect);

    void OnSelect(SelectEnterEventArgs args) => onPressed.Invoke();



    void Start()
    {
        // Set the initial material to ButtonDisabled
        this.transform.parent.GetComponentInChildren<Renderer>().material = ButtonDisabled;
    }
    void FixedUpdate()
    {
        ButtonCooldown -= 1;
    }

    public void ButtonToggle() 
    {
        if (ButtonCooldown <= 0)
        {
            // Code to lower the button
            if (buttonenabled == false)
            {
                this.transform.parent.localPosition = new Vector3(this.transform.parent.localPosition.x, this.transform.parent.localPosition.y - 0.01f, this.transform.parent.localPosition.z);
                this.transform.parent.GetComponentInChildren<Renderer>().material = ButtonEnabled;
                buttonenabled = true;
            }
            else if (buttonenabled == true)
            {
                this.transform.parent.localPosition = new Vector3(this.transform.parent.localPosition.x, this.transform.parent.localPosition.y + 0.01f, this.transform.parent.localPosition.z);
                this.transform.parent.GetComponentInChildren<Renderer>().material = ButtonDisabled;
                buttonenabled = false;
            }
        }
    }
}
