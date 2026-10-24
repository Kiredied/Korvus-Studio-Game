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

    // input bar 2d
    Vector3 InputBar2DScale;
    Vector3 InputBar2DLocation;
    
    // debug progress/completion bar 
    Vector3 TempProgressBar2DScale;
    Vector3 TempProgressBar2DLocation;
    
    // progress bar canister 1
    Vector3 RealProgressBar3D1Scale;
    Vector3 RealProgressBar3D1Location;
    // progress bar canister 2
    
    Vector3 RealProgressBar3D2Scale;
    Vector3 RealProgressBar3D2Location;
    
    // progress bar canister 3
    Vector3 RealProgressBar3D3Scale;
    Vector3 RealProgressBar3D3Location;
    
    // UI Values
    [SerializeField] float TempProgressBar2DValue = 0f;
    [SerializeField] float MaxTempProgressBar2DValue = 500f;
    
    // Canister values
    [SerializeField] float RealProgressBar3D1Value = 0f;
    [SerializeField] float RealProgressBar3D2Value = 0f;
    [SerializeField] float RealProgressBar3D3Value = 0f;
    // Max canister value
    [SerializeField] float MaxRealProgressBar3DValue = 500f;
    
    // visible values ingame
    float TempProgressBar2DShown = 0f;
    float RealProgressBar3D1Shown = 0f;
    float RealProgressBar3D2Shown = 0f;
    float RealProgressBar3D3Shown = 0f;

    void Awake()
    {
        // inputbar: set current scale and position to variable to save for later
        InputBar2DScale = InputBar2D.transform.localScale;
        InputBar2DLocation = InputBar2D.transform.localPosition;

        // debug progress bar:set current scale and position to variable to save for later
        TempProgressBar2DScale = TempProgressBar2D.transform.localScale;
        TempProgressBar2DLocation = TempProgressBar2D.transform.localPosition;
        // reset values debug progress bar
        TempProgressBar2DValue = 0f;
        TempProgressBar2DShown = 0f;
        
        // canister1: set current scale and position to variable to save for later
        RealProgressBar3D1Scale = RealProgressBar3D1.transform.localScale;
        RealProgressBar3D1Location = RealProgressBar3D1.transform.localPosition;
        
        // canister2: set current scale and position to variable to save for later
        RealProgressBar3D2Scale = RealProgressBar3D2.transform.localScale;
        RealProgressBar3D2Location = RealProgressBar3D2.transform.localPosition;
        
        // canister3: set current scale and position to variable to save for later
        RealProgressBar3D3Scale = RealProgressBar3D3.transform.localScale;
        RealProgressBar3D3Location = RealProgressBar3D3.transform.localPosition;
        
        // reset values canisters 1-3
        RealProgressBar3D1Value = 0f;
        RealProgressBar3D1Shown = 0f;
        RealProgressBar3D2Value = 0f;
        RealProgressBar3D2Shown = 0f;
        RealProgressBar3D3Value = 0f;
        RealProgressBar3D3Shown = 0f;
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
        
        //if 1
        RealProgressBar3D1.transform.localScale = new Vector3((RealProgressBar3D1Shown / RealMaxprogress), RealProgressBar3D1Scale.y, RealProgressBar3D1Scale.z);
        RealProgressBar3D1.transform.localPosition = new Vector3((RealProgressBar3D1Shown - 1) / (RealMaxprogress * 2f), RealProgressBar3D1Location.y, RealProgressBar3D1Location.z);
        
        RealProgressBar3D2.transform.localScale = new Vector3((RealProgressBar3D2Shown / RealMaxprogress), RealProgressBar3D2Scale.y, RealProgressBar3D2Scale.z);
        RealProgressBar3D2.transform.localPosition = new Vector3((RealProgressBar3D2Shown - 1) / (RealMaxprogress * 2f), RealProgressBar3D2Location.y, RealProgressBar3D2Location.z);
        
        RealProgressBar3D3.transform.localScale = new Vector3((RealProgressBar3D3Shown / RealMaxprogress), RealProgressBar3D3Scale.y, RealProgressBar3D3Scale.z);
        RealProgressBar3D3.transform.localPosition = new Vector3((RealProgressBar3D3Shown - 1) / (RealMaxprogress * 2f), RealProgressBar3D3Location.y, RealProgressBar3D3Location.z);

        
        if (WheelRotation >= 90)
        {
            TempProgressBar2DValue += 1f;
            // if (canister1insocket) {
            RealProgressBar3D1Value += 1f;
            // } else if (canister2insocket) {
            RealProgressBar3D2Value += 1f;
            // } else if (canister3insocket) {
            RealProgressBar3D3Value += 1f;
            // }
        }

        
        TempProgressBar2DShown = Mathf.InverseLerp(0f, MaxTempProgressBar2DValue, TempProgressBar2DValue) * 100;
        RealProgressBar3D1Shown = Mathf.InverseLerp(0f, MaxRealProgressBar3DValue, RealProgressBar3D1Value) * 100;
        RealProgressBar3D2Shown = Mathf.InverseLerp(0f, MaxRealProgressBar3DValue, RealProgressBar3D2Value) * 100;
        RealProgressBar3D3Shown = Mathf.InverseLerp(0f, MaxRealProgressBar3DValue, RealProgressBar3D3Value) * 100;
    }
}
