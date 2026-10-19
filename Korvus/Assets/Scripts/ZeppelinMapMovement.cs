using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ZeppelinMapMovement : MonoBehaviour
{
    static public Rigidbody RigidBody2DZeppelin;
    public float speed;
    public float turnSpeed;
    static public Quaternion DeathRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RigidBody2DZeppelin = GetComponentInChildren<Rigidbody>();
        buttontest.ButtonForwardEnabled = false;
        buttontest.ButtonLeftEnabled = false;
        buttontest.ButtonRightEnabled = false;

    }


    void FixedUpdate()
    {
        if (!DangerZone2D.DeathActivated)
        {
            if (!buttontest.ButtonForwardEnabled)
            {
                // button forward not pressed
                RigidBody2DZeppelin.linearVelocity = RigidBody2DZeppelin.transform.forward * speed / 3;
            }
            else
            {
                // button forward pressed
                RigidBody2DZeppelin.linearVelocity = RigidBody2DZeppelin.transform.forward * speed;
            }


            var mult = buttontest.ButtonForwardEnabled? speed : speed / 3;
            RigidBody2DZeppelin.linearVelocity = RigidBody2DZeppelin.transform.forward * mult;



            if (buttontest.ButtonLeftEnabled && buttontest.ButtonRightEnabled)
            {
                // both steering buttons pressed, cancel out rotation
                RigidBody2DZeppelin.angularVelocity = Vector3.zero;
            }
            else
            {
                if (!buttontest.ButtonLeftEnabled && !buttontest.ButtonRightEnabled)
                {
                    // no steering buttons pressed, no rotation
                    RigidBody2DZeppelin.angularVelocity = Vector3.zero;
                }
                else
                {
                    if (buttontest.ButtonLeftEnabled)
                    {
                        // left steering button pressed, rotate left
                        RigidBody2DZeppelin.angularVelocity = RigidBody2DZeppelin.transform.up * -(turnSpeed);
                    }
                    if (buttontest.ButtonRightEnabled)
                    {
                        // right steering button pressed, rotate right
                        RigidBody2DZeppelin.angularVelocity = RigidBody2DZeppelin.transform.up * turnSpeed;
                    }
                }
            }



        }
        else 
        {
            RigidBody2DZeppelin.rotation = Quaternion.RotateTowards(RigidBody2DZeppelin.rotation, DeathRotation, turnSpeed / 3);
            RigidBody2DZeppelin.angularVelocity = Vector3.zero;
            RigidBody2DZeppelin.linearVelocity = RigidBody2DZeppelin.transform.forward * speed / 3;
        }

        
    }


    static public void Death()
    {
        buttontest.buttonforward.GetComponent<XRSimpleInteractable>().enabled = false;
        buttontest.buttonleft.GetComponent<XRSimpleInteractable>().enabled = false;
        buttontest.buttonright.GetComponent<XRSimpleInteractable>().enabled = false;
        buttontest.resetbuttons = true;
        buttontest.ButtonForwardEnabled = false;
        buttontest.ButtonLeftEnabled = false;
        buttontest.ButtonRightEnabled = false;
        
        Debug.Log("Player deader than a corpse!!11!!!1!1 ohnoooooo :(((((");
        DangerZone2D.ConsiousnessRemover = true;
    }
}
