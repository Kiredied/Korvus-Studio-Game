using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DarknessController : MonoBehaviour
{

    private bool log = false;
    private bool mainmenulog = false;
    private bool revertlog = false;

    //[SerializeField] GameObject DarknessMainMenu;
    //[SerializeField] GameObject DarknessGame;

    [Range(1f, 100f)] static public int DarknessSpeed = 10; 
    static private float darknessLevel = -10f; 
    ColorAdjustments colorAdjustments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Volume volume = this.GetComponent<Volume>();
        if (volume != null && volume.profile != null)
        {
            if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments)) {}
        }
        
    }

   
    void FixedUpdate()
    {
        // 50 fps

        if (DangerZone2D.ConsiousnessRemover)
        { 
            if (!log) Debug.Log("Fading consciousness. (Post-Processing)"); log = true;

            colorAdjustments.postExposure.value = ((colorAdjustments.postExposure.value * (101 - DarknessSpeed) + darknessLevel) / (102 - DarknessSpeed));
        }

        if (MainmenuCode.TurnDarkBool)
        {
            
        }

        if (DangerZone2D.DarknessRemover)
        {
            
        }
    }
}
