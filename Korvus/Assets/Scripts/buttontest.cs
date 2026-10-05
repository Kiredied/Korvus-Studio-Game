using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class buttontest : MonoBehaviour
{

    [SerializeField] XRSimpleInteractable interactable;
    public UnityEvent onPressed;


    bool buttonenabled = false;
    int ButtonCooldown = 15;
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
        ButtonCooldown = ButtonCooldown - 1;
    }

    public void ButtonToggle() 
    {
        
            // Code to lower the button
            if (buttonenabled == false && ButtonCooldown <= 0)
            {
                this.transform.parent.transform.Find("Mesh").localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.1f, this.transform.localPosition.z);
                this.transform.parent.GetComponentInChildren<Renderer>().material = ButtonEnabled;
                buttonenabled = true;
                ButtonCooldown = 15;
            }
            else if (buttonenabled == true && ButtonCooldown <= 0)
            {
                this.transform.parent.transform.Find("Mesh").localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 0.1f, this.transform.localPosition.z);
                this.transform.parent.GetComponentInChildren<Renderer>().material = ButtonDisabled;
                buttonenabled = false;
                ButtonCooldown = 15;
            }
        
    }
}
