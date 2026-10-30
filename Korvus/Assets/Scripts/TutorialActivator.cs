using UnityEngine;

public class TutorialActivator : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;

    private bool activated = false;

    void Update()
    {
        if (DangerZone2D.DangerZone && !activated)
        {
            prefab1.SetActive(true);
            prefab2.SetActive(true);

            activated = true;
        }
    }
}