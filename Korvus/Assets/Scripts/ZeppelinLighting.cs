using NUnit.Framework;
using UnityEngine;

public class ZeppelinLighting : MonoBehaviour
{
    public Light lightComponent;
    [SerializeField, UnityEngine.Range(1f,10f)] private float ColorChangeSpeedSlider = 5f;
    private float ColorChangeSpeed;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        // DangerZone2D.PreviousDangerZone = true;
        // DangerZone2D.DangerZone = true;
        // DangerZone2D.DeadZone = true;

    }

    void FixedUpdate()
    {
        ColorChangeSpeed = 11f - ColorChangeSpeedSlider;
        if (lightComponent != null)
        {
            if (DangerZone2D.DeathActivated)
            {
                Color color = lightComponent.color;
                color.r = ((color.r * ColorChangeSpeed) + (25f / 255f)) / (ColorChangeSpeed + 1);
                color.g = ((color.g * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                color.b = ((color.b * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                lightComponent.color = color;
                //lightComponent.color = new Color(25f / 255f, 0, 0);

                FogSteering.SetEmissionColor(new Color(0, 0, 0, 200f / 255f), new Color(0, 0, 0, 200f / 255f));
            }
            else if (DangerZone2D.DeadZone)
            {
                Color color = lightComponent.color;
                color.r = ((color.r * ColorChangeSpeed) + (75f / 255f)) / (ColorChangeSpeed + 1);
                color.g = ((color.g * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                color.b = ((color.b * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                lightComponent.color = color;
                //lightComponent.color = new Color(75f / 255f, 0, 0);

                FogSteering.SetEmissionColor(new Color(5f / 255f, 0, 0, 150f / 255f), new Color(10f / 255f, 0, 0, 150f / 255f));
            }
            else if (DangerZone2D.DangerZone)
            {
                Color color = lightComponent.color;
                color.r = ((color.r * ColorChangeSpeed) + (115f / 255f)) / (ColorChangeSpeed + 1);
                color.g = ((color.g * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                color.b = ((color.b * ColorChangeSpeed) + 0) / (ColorChangeSpeed + 1);
                lightComponent.color = color;
                //lightComponent.color = new Color(115f / 255f, 0, 0);

                FogSteering.SetEmissionColor(new Color(15f / 255f, 0, 0, 100f / 255f), new Color(20f / 255f, 0, 0, 100f / 255f));
            }
            else if (DangerZone2D.PreviousDangerZone)
            {
                Color color = lightComponent.color;
                color.r = ((color.r * ColorChangeSpeed) + (120f / 255f)) / (ColorChangeSpeed + 1);
                color.g = ((color.g * ColorChangeSpeed) + (120f / 255f)) / (ColorChangeSpeed + 1);
                color.b = ((color.b * ColorChangeSpeed) + (65f / 255f)) / (ColorChangeSpeed + 1);
                lightComponent.color = color;
                //lightComponent.color = new Color(120f / 255f, 120f / 255f, 65f / 255f);

                FogSteering.SetEmissionColor(new Color(77f / 255f, 77f / 255f, 50f / 255f, 55f / 255f), new Color(60f / 255f, 60f / 255f, 40f / 255f, 55f / 255f));
            }
            else
            {
                Color color = lightComponent.color;
                color.r = ((color.r * ColorChangeSpeed) + (150f / 255f)) / (ColorChangeSpeed + 1);
                color.g = ((color.g * ColorChangeSpeed) + (150f / 255f)) / (ColorChangeSpeed + 1);
                color.b = ((color.b * ColorChangeSpeed) + (130f / 255f)) / (ColorChangeSpeed + 1);
                lightComponent.color = color;
                //lightComponent.color = new Color(150f / 255f, 150f / 255f, 130f / 255f);

                FogSteering.SetEmissionColor(new Color(77f / 255f, 77f / 255f, 77f / 255f, 50f / 255f), new Color(60f / 255f, 60f / 255f, 60f / 255f, 50f / 255f));
            }
        }
    }
}
