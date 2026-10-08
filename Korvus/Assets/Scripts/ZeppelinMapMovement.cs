using UnityEngine;

public class ZeppelinMapMovement : MonoBehaviour
{
    public Rigidbody RigidBody2DZeppelin;
    public float speed;
    public float turnSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttontest.ButtonForwardEnabled = false;
        buttontest.ButtonLeftEnabled = false;
        buttontest.ButtonRightEnabled = false;

    }


    void FixedUpdate()
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
}
