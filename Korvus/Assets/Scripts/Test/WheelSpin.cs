using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] XRBaseInteractable[] handles;
    [SerializeField] Vector3 localAxis = Vector3.forward;
    [SerializeField] float sensitivity = 1f;

    [Tooltip("Higher = the wheel slows down faster after release")]
    [SerializeField] float friction = 1.5f;

    [Tooltip("0 = very smooth release speed, 1 = raw hand speed")]
    [SerializeField, Range(0f, 1f)] float velocitySmoothing = 0.5f;

    IXRSelectInteractor interactor;
    Vector3 localGrabOffset;   // where the grabbed cube sits in the hand's space
    float lastAngle;
    float angularVelocity;     // degrees per second

    Vector3 WorldAxis => transform.TransformDirection(localAxis).normalized;

    [SerializeField] public float TotalRotation;

    [SerializeField] float minRotation = -180f;
    [SerializeField] float maxRotation = 720f;

    public void ResetRotation()
    {
        TotalRotation = 0f;
    }

    void OnEnable()
    {
        foreach (var h in handles)
        {
            h.selectEntered.AddListener(OnEnter);
            h.selectExited.AddListener(OnExit);
        }
    }

    void OnDisable()
    {
        foreach (var h in handles)
        {
            h.selectEntered.RemoveListener(OnEnter);
            h.selectExited.RemoveListener(OnExit);
        }
    }

    void OnEnter(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject;
        Transform hand = interactor.transform;

        // Remember where the cube is relative to the hand, so the hand
        // carries that point with it, including wrist rotation.
        localGrabOffset = hand.InverseTransformPoint(args.interactableObject.transform.position);

        lastAngle = AngleOf(GrabPoint());
        angularVelocity = 0f; // stop any coasting from a previous grab
    }

    void OnExit(SelectExitEventArgs args)
    {
        if (interactor == args.interactorObject) interactor = null;
        Debug.Log($"Released. angularVelocity = {angularVelocity}");
    }

    void LateUpdate()
    {
        float dt = Time.deltaTime;

        if (interactor != null)
        {
            float angle = AngleOf(GrabPoint());
            float delta = Mathf.DeltaAngle(lastAngle, angle) * sensitivity;
            delta = ClampStep(delta);
            lastAngle = angle;

            if (dt > 0f)
                angularVelocity = Mathf.Lerp(angularVelocity, delta / dt, velocitySmoothing);

            transform.Rotate(WorldAxis, delta, Space.World);
            TotalRotation += delta;
            Debug.Log($"delta={delta:F3}  total={TotalRotation:F3}  inspectorZ={transform.localEulerAngles.z:F3}");
        }
        else if (Mathf.Abs(angularVelocity) > 0.05f)
        {
            // Released: keep spinning and slow down.
            float step = angularVelocity * dt;

            // Limit the step. If it had to be cut, the wheel is at a limit, so stop coasting.
            float clamped = ClampStep(step);
            if (Mathf.Abs(clamped - step) > 0.0001f)
                angularVelocity = 0f;
            step = clamped;

            transform.Rotate(WorldAxis, step, Space.World);
            TotalRotation += step;   // add the same amount that was actually rotated

            // Slow down for the next frame.
            angularVelocity *= Mathf.Exp(-friction * dt);
        }
        else
        {
            angularVelocity = 0f;
        }
    }

    Vector3 GrabPoint()
    {
        return interactor.transform.TransformPoint(localGrabOffset);
    }

    float AngleOf(Vector3 worldPoint)
    {
        Vector3 axis = WorldAxis;
        Vector3 offset = Vector3.ProjectOnPlane(worldPoint - transform.position, axis);

        Vector3 reference = Vector3.ProjectOnPlane(Vector3.up, axis);
        if (reference.sqrMagnitude < 0.001f)
            reference = Vector3.ProjectOnPlane(Vector3.forward, axis);

        return Vector3.SignedAngle(reference, offset, axis);
    }

    float ClampStep(float step)
    {
        float target = Mathf.Clamp(TotalRotation + step, minRotation, maxRotation);
        return target - TotalRotation;
    }
}
