using UnityEngine;

public class TutorialActivator : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;

    private bool activated = false;

    void Update()
    {
        if (DangerZone2D.DangerZone && !activated && !DangerZone2D.DangerWall)
        {
            // i added "&& !DangerZone2D.DangerWall" so the tutorial doesnt activate when going in the damgerwall, only the dangerzone
            prefab1.SetActive(true);
            prefab2.SetActive(true);

            activated = true;
        }
    }
}