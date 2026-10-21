using UnityEngine;

public class ExtractorCode : MonoBehaviour
{
    float WheelRotation = 0f;
    [SerializeField] GameObject EffectivenessBar2D;
    [SerializeField] GameObject TempProgressBar3D;
    [SerializeField] float tempMax = 200f;


    Vector3 EffectivenessBar2DScale;
    Vector3 EffectivenessBar2DLocation;

    void Awake()
    {
        EffectivenessBar2DScale = EffectivenessBar2D.transform.localScale;
        EffectivenessBar2DLocation = EffectivenessBar2D.transform.localPosition;
    }


    void Update()
    {
        WheelRotation = WheelSpin.RotationToPercentage;


        EffectivenessBar2D.transform.localScale = new Vector3(WheelRotation / tempMax, EffectivenessBar2DScale.y, EffectivenessBar2DScale.z);
        EffectivenessBar2D.transform.localPosition = new Vector3((WheelRotation - 1) / (tempMax * 2f), EffectivenessBar2DLocation.y, EffectivenessBar2DLocation.z);

    }
}
