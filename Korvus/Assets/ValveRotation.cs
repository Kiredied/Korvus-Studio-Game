using UnityEngine;

public class ValveRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();

        hinge.useLimits = true;
        JointLimits limits = hinge.limits;
        limits.min = 90f;
        limits.max = 180f;
        hinge.limits = limits;
    }
}
