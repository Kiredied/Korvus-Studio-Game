using UnityEngine;

public class ValveRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();
        hinge.useLimits = false;
    }
}
