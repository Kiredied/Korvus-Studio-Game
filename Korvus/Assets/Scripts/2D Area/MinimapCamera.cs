using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the parent's parent rotation in euler angles
        Vector3 parentEuler = transform.parent.parent.rotation.eulerAngles;
        // Set this transform's rotation to match the parent's parent x and y, keep z as is
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, parentEuler.y + 90, parentEuler.z);
    }
}
