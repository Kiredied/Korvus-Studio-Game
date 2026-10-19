using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class FogSteering : MonoBehaviour
{
    [SerializeField] private ParticleSystem fogForwardSlow;
    [SerializeField] private ParticleSystem fogForward;
    [SerializeField] private ParticleSystem fogLeft;
    [SerializeField] private ParticleSystem fogLeftSlow;
    [SerializeField] private ParticleSystem fogRight;
    [SerializeField] private ParticleSystem fogRightSlow;

    [SerializeField] private GameObject fogForwardSlowParent;
    [SerializeField] private GameObject fogForwardParent;
    [SerializeField] private GameObject fogLeftParent;
    [SerializeField] private GameObject fogLeftSlowParent;
    [SerializeField] private GameObject fogRightParent;
    [SerializeField] private GameObject fogRightSlowParent;

    [SerializeField] private VisualEffect testVisualEffect;

    private float defaultEmissionRateForwardSlow;
    private float defaultEmissionRateForward;
    private float defaultEmissionRateLeft;
    private float defaultEmissionRateLeftSlow;
    private float defaultEmissionRateRight;
    private float defaultEmissionRateRightSlow;

    private bool isFast = false;
    private bool isSlow = true;

    void Start()
    {
        // Store default emission rates
        defaultEmissionRateForwardSlow = fogForwardSlow.emission.rateOverTime.constant;
        defaultEmissionRateForward = fogForward.emission.rateOverTime.constant;
        defaultEmissionRateLeft = fogLeft.emission.rateOverTime.constant;
        defaultEmissionRateLeftSlow = fogLeftSlow.emission.rateOverTime.constant;
        defaultEmissionRateRight = fogRight.emission.rateOverTime.constant;
        defaultEmissionRateRightSlow = fogRightSlow.emission.rateOverTime.constant;

        // Start all particle systems playing
        fogForwardSlow.Play();
        fogForward.Play();
        fogLeft.Play();
        fogLeftSlow.Play();
        fogRight.Play();
        fogRightSlow.Play();

        // Initially disable all emission
        testVisualEffect.SetFloat("ForwardEmissionSpeed", 0.8f);
        testVisualEffect.SetFloat("SidewardEmissionSpeed", 0f);
        SetEmissionRate(fogForwardSlow, 0f);
        SetEmissionRate(fogForward, 0f);
        SetEmissionRate(fogLeft, 0f);
        SetEmissionRate(fogLeftSlow, 0f);
        SetEmissionRate(fogRight, 0f);
        SetEmissionRate(fogRightSlow, 0f);

        testVisualEffect.SetFloat("ColorRMin", 77f/255f);
        testVisualEffect.SetFloat("ColorGMin", 77f/255f);
        testVisualEffect.SetFloat("ColorBMin", 77f/255f);
        testVisualEffect.SetFloat("ColorRMax", 60f/255f);
        testVisualEffect.SetFloat("ColorGMax", 60f/255f);
        testVisualEffect.SetFloat("ColorBMax", 60f/255f);


    }

    void FixedUpdate()
    {
        if (!buttontest.ButtonForwardEnabled)
        {
            // button forward not pressed
            isSlow = true;
            isFast = false;
        }
        else
        {
            // button forward pressed
            isSlow = false;
            isFast = true;
        }

        if (buttontest.ButtonLeftEnabled && buttontest.ButtonRightEnabled)
        {
            // both steering buttons pressed, cancel out rotation
            if (isSlow)
            {
                /*SetEmissionRate(fogForwardSlow, defaultEmissionRateForwardSlow);
                SetEmissionRate(fogForward, 0f);
                SetEmissionRate(fogLeft, 0f);
                SetEmissionRate(fogLeftSlow, 0f);
                SetEmissionRate(fogRight, 0f);
                SetEmissionRate(fogRightSlow, 0f);*/
                testVisualEffect.SetFloat("ForwardEmissionSpeed", .5f);
                testVisualEffect.SetFloat("SidewardEmissionSpeed", 0f);
            }
            else if (isFast)
            {
                /*SetEmissionRate(fogForwardSlow, 0f);
                SetEmissionRate(fogForward, defaultEmissionRateForward);
                SetEmissionRate(fogLeft, 0f);
                SetEmissionRate(fogLeftSlow, 0f);
                SetEmissionRate(fogRight, 0f);
                SetEmissionRate(fogRightSlow, 0f);*/
                testVisualEffect.SetFloat("ForwardEmissionSpeed", 1f);
                testVisualEffect.SetFloat("SidewardEmissionSpeed", 0f);
            }
        }
        else
        {
            if (!buttontest.ButtonLeftEnabled && !buttontest.ButtonRightEnabled)
            {
                // no steering buttons pressed, no rotation
                if (isSlow)
                {
                    /*SetEmissionRate(fogForwardSlow, defaultEmissionRateForwardSlow);
                    SetEmissionRate(fogForward, 0f);
                    SetEmissionRate(fogLeft, 0f);
                    SetEmissionRate(fogLeftSlow, 0f);
                    SetEmissionRate(fogRight, 0f);
                    SetEmissionRate(fogRightSlow, 0f);*/
                    testVisualEffect.SetFloat("ForwardEmissionSpeed", .5f);
                    testVisualEffect.SetFloat("SidewardEmissionSpeed", 0f);
                }
                else if (isFast)
                {
                    /*SetEmissionRate(fogForwardSlow, 0f);
                    SetEmissionRate(fogForward, defaultEmissionRateForward);
                    SetEmissionRate(fogLeft, 0f);
                    SetEmissionRate(fogLeftSlow, 0f);
                    SetEmissionRate(fogRight, 0f);
                    SetEmissionRate(fogRightSlow, 0f);*/
                    testVisualEffect.SetFloat("ForwardEmissionSpeed", 1f);
                    testVisualEffect.SetFloat("SidewardEmissionSpeed", 0f);
                }
            }
            else
            {
                if (buttontest.ButtonLeftEnabled)
                {
                    // left steering button pressed, rotate left
                    if (isSlow)
                    {
                        /*SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, defaultEmissionRateLeftSlow);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, 0f);*/
                        testVisualEffect.SetFloat("ForwardEmissionSpeed", .5f);
                        testVisualEffect.SetFloat("SidewardEmissionSpeed", -.5f);
                    }
                    else if (isFast)
                    {
                        /*SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, defaultEmissionRateLeft);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, 0f);*/
                        testVisualEffect.SetFloat("ForwardEmissionSpeed", 1f);
                        testVisualEffect.SetFloat("SidewardEmissionSpeed", -.5f);
                    }
                }
                if (buttontest.ButtonRightEnabled)
                {
                    // right steering button pressed, rotate right
                    if (isSlow)
                    {
                        /*SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, defaultEmissionRateRightSlow);*/
                        testVisualEffect.SetFloat("ForwardEmissionSpeed", .5f);
                        testVisualEffect.SetFloat("SidewardEmissionSpeed", .5f);
                    }
                    else if (isFast)
                    {
                        /*SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, defaultEmissionRateRight);
                        SetEmissionRate(fogRightSlow, 0f);*/
                        testVisualEffect.SetFloat("ForwardEmissionSpeed", 1f);
                        testVisualEffect.SetFloat("SidewardEmissionSpeed", .5f);
                    }
                }
            }
        }
    }

    private void SetEmissionRate(ParticleSystem particleSystem, float rate)
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = rate;
        CurrentParticleSystem = particleSystem.gameObject;
        SetEmissionColor(CurrentColor1, CurrentColor2);
    }


    static private GameObject CurrentParticleSystem;
    static private UnityEngine.Color CurrentColor1 = new UnityEngine.Color(77f/255f,77f/255f,77f/255f,50f/255f);
    static private UnityEngine.Color CurrentColor2 = new UnityEngine.Color(60f/255f,60f/255f,60f/255f,50f/255f);

    


    public void SetEmissionColor(UnityEngine.Color color1, UnityEngine.Color color2)
    {
        if (CurrentParticleSystem == null)
        {
            Debug.LogWarning("CurrentParticleSystem is not set. Cannot change emission color.");
            return;
        }
        var main = CurrentParticleSystem.GetComponent<ParticleSystem>().main;
        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
        testVisualEffect.SetFloat("ColorRMin", color1.r);
        testVisualEffect.SetFloat("ColorGMin", color1.g);
        testVisualEffect.SetFloat("ColorBMin", color1.b);
        testVisualEffect.SetFloat("ColorRMax", color2.r);
        testVisualEffect.SetFloat("ColorGMax", color2.g);
        testVisualEffect.SetFloat("ColorBMax", color2.b);

        CurrentColor1 = color1;
        CurrentColor2 = color2;
    }
}
