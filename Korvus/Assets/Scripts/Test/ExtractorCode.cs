using UnityEngine;

public class ExtractorCode : MonoBehaviour
{
    float WheelRotation = 0f;
    [SerializeField] GameObject InputBar2D;
    [SerializeField] GameObject TempProgressBar2D;
    [SerializeField] GameObject RealProgressBar3D1;
    [SerializeField] GameObject RealProgressBar3D2;
    [SerializeField] GameObject RealProgressBar3D3;
    
    [SerializeField] float tempMax = 150f;
    [SerializeField] float tempMaxprogress = 150f;
    [SerializeField] float RealMaxprogress = 150f;
    
    float timing;

    Vector3 InputBar2DScale;
    Vector3 InputBar2DLocation;

    Vector3 TempProgressBar2DScale;
    Vector3 TempProgressBar2DLocation;
    
    Vector3 RealProgressBar3DScale;
    Vector3 RealProgressBar3DLocation;

    [SerializeField] float TempProgressBar2DValue = 0f;
    [SerializeField] float MaxTempProgressBar2DValue = 500f;
    
    [SerializeField] float RealProgressBar3DValue = 0f;
    [SerializeField] float MaxRealProgressBar3DValue = 500f;
    
    
    float TempProgressBar2DShown = 0f;
    float RealProgressBar3DShown = 0f;

    void Awake()
    {
        InputBar2DScale = InputBar2D.transform.localScale;
        InputBar2DLocation = InputBar2D.transform.localPosition;

        TempProgressBar2DScale = TempProgressBar2D.transform.localScale;
        TempProgressBar2DLocation = TempProgressBar2D.transform.localPosition;
        TempProgressBar2DValue = 0f;
        TempProgressBar2DShown = 0f;
        
        //if 1
        RealProgressBar3DScale = RealProgressBar3D1.transform.localScale;
        RealProgressBar3DLocation = RealProgressBar3D1.transform.localPosition;
        
        //if 2
        //RealProgressBar3DScale = RealProgressBar3D2.transform.localScale;
        //RealProgressBar3DLocation = RealProgressBar3D2.transform.localPosition;
        
        //if 3
        //RealProgressBar3DScale = RealProgressBar3D3.transform.localScale;
        //RealProgressBar3DLocation = RealProgressBar3D3.transform.localPosition;
        
        
        RealProgressBar3DValue = 0f;
        RealProgressBar3DShown = 0f;
    }


    void FixedUpdate()
    {
        WheelRotation = WheelSpin.RotationToPercentage;
        
        
        /*
        //Debug
        if (timing >= 50)
        {
            Debug.Log("InputBar2D scalex: " + (WheelRotation / tempMax));
            Debug.Log("TempProgressBar2D scalex: " + (TempProgressBar2DShown / tempMaxprogress));
            timing = 0;
        }
        else timing += 1;
        */

        InputBar2D.transform.localScale = new Vector3((WheelRotation / tempMax), InputBar2DScale.y, InputBar2DScale.z);
        InputBar2D.transform.localPosition = new Vector3((WheelRotation - 1) / (tempMax * 2f), InputBar2DLocation.y, InputBar2DLocation.z);

        //debug
        TempProgressBar2D.transform.localScale = new Vector3((TempProgressBar2DShown / tempMaxprogress), TempProgressBar2DScale.y, TempProgressBar2DScale.z);
        TempProgressBar2D.transform.localPosition = new Vector3((TempProgressBar2DShown - 1) / (tempMaxprogress * 2f), TempProgressBar2DLocation.y, TempProgressBar2DLocation.z);
        //
        
        RealProgressBar3D1.transform.localScale = new Vector3((RealProgressBar3DShown / RealMaxprogress), RealProgressBar3DScale.y, RealProgressBar3DScale.z);
        RealProgressBar3D1.transform.localPosition = new Vector3((RealProgressBar3DShown - 1) / (RealMaxprogress * 2f), RealProgressBar3DLocation.y, RealProgressBar3DLocation.z);

        
        if (WheelRotation >= 90)
        {
            TempProgressBar2DValue += 1f;
            RealProgressBar3DValue += 1f;
            
        }

        RealProgressBar3DShown = Mathf.InverseLerp(0f, MaxRealProgressBar3DValue, RealProgressBar3DValue) * 100;
        TempProgressBar2DShown = Mathf.InverseLerp(0f, MaxTempProgressBar2DValue, TempProgressBar2DValue) * 100;
    }
}
