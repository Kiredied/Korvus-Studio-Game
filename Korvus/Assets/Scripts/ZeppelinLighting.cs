using UnityEngine;

public class ZeppelinLighting : MonoBehaviour
{
    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        // DangerZone2D.PreviousDangerZone = true;
        // DangerZone2D.DangerZone = true;
        // DangerZone2D.DeadZone = true;
    }

    void FixedUpdate()
    {
        if (DangerZone2D.PreviousDangerZone)
        {
            lightComponent.color = new Color(120f/255f, 120f/255f, 65f/255f);
        }
        else if (DangerZone2D.DangerZone)
        {
            lightComponent.color = new Color(115f/255f, 0, 0);
        }
        else if (DangerZone2D.DeadZone)
        {
            lightComponent.color = Color.black;
        }
        else
        {
            lightComponent.color = new Color(150f/255f, 150f/255f, 130f/255f);
        }
    }
}
