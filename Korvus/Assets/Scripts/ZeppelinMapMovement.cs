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
            RigidBody2DZeppelin.linearVelocity = Vector3.zero;
        }
        else
        {
          RigidBody2DZeppelin.linearVelocity = RigidBody2DZeppelin.transform.forward * speed;
        }


        if (buttontest.ButtonLeftEnabled && buttontest.ButtonRightEnabled)
        {
            RigidBody2DZeppelin.angularVelocity = Vector3.zero;
        }
        else
        {
            if (!buttontest.ButtonLeftEnabled && !buttontest.ButtonRightEnabled)
            {
                RigidBody2DZeppelin.angularVelocity = Vector3.zero;
            }
            else 
            {
                if (buttontest.ButtonLeftEnabled)
                {
                    RigidBody2DZeppelin.angularVelocity = RigidBody2DZeppelin.transform.up * -(turnSpeed);
                }
                if (buttontest.ButtonRightEnabled)
                {
                    RigidBody2DZeppelin.angularVelocity = RigidBody2DZeppelin.transform.up * turnSpeed;
                }
            }
        }


        
    }
}
