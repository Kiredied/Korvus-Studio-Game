using UnityEngine;

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
        SetEmissionRate(fogForwardSlow, 0f);
        SetEmissionRate(fogForward, 0f);
        SetEmissionRate(fogLeft, 0f);
        SetEmissionRate(fogLeftSlow, 0f);
        SetEmissionRate(fogRight, 0f);
        SetEmissionRate(fogRightSlow, 0f);
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
                SetEmissionRate(fogForwardSlow, defaultEmissionRateForwardSlow);
                SetEmissionRate(fogForward, 0f);
                SetEmissionRate(fogLeft, 0f);
                SetEmissionRate(fogLeftSlow, 0f);
                SetEmissionRate(fogRight, 0f);
                SetEmissionRate(fogRightSlow, 0f);
            }
            else if (isFast)
            {
                SetEmissionRate(fogForwardSlow, 0f);
                SetEmissionRate(fogForward, defaultEmissionRateForward);
                SetEmissionRate(fogLeft, 0f);
                SetEmissionRate(fogLeftSlow, 0f);
                SetEmissionRate(fogRight, 0f);
                SetEmissionRate(fogRightSlow, 0f);
            }
        }
        else
        {
            if (!buttontest.ButtonLeftEnabled && !buttontest.ButtonRightEnabled)
            {
                // no steering buttons pressed, no rotation
                if (isSlow)
                {
                    SetEmissionRate(fogForwardSlow, defaultEmissionRateForwardSlow);
                    SetEmissionRate(fogForward, 0f);
                    SetEmissionRate(fogLeft, 0f);
                    SetEmissionRate(fogLeftSlow, 0f);
                    SetEmissionRate(fogRight, 0f);
                    SetEmissionRate(fogRightSlow, 0f);
                }
                else if (isFast)
                {
                    SetEmissionRate(fogForwardSlow, 0f);
                    SetEmissionRate(fogForward, defaultEmissionRateForward);
                    SetEmissionRate(fogLeft, 0f);
                    SetEmissionRate(fogLeftSlow, 0f);
                    SetEmissionRate(fogRight, 0f);
                    SetEmissionRate(fogRightSlow, 0f);
                }
            }
            else
            {
                if (buttontest.ButtonLeftEnabled)
                {
                    // left steering button pressed, rotate left
                    if (isSlow)
                    {
                        SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, defaultEmissionRateLeftSlow);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, 0f);
                    }
                    else if (isFast)
                    {
                        SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, defaultEmissionRateLeft);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, 0f);
                    }
                }
                if (buttontest.ButtonRightEnabled)
                {
                    // right steering button pressed, rotate right
                    if (isSlow)
                    {
                        SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, 0f);
                        SetEmissionRate(fogRightSlow, defaultEmissionRateRightSlow);
                    }
                    else if (isFast)
                    {
                        SetEmissionRate(fogForwardSlow, 0f);
                        SetEmissionRate(fogForward, 0f);
                        SetEmissionRate(fogLeft, 0f);
                        SetEmissionRate(fogLeftSlow, 0f);
                        SetEmissionRate(fogRight, defaultEmissionRateRight);
                        SetEmissionRate(fogRightSlow, 0f);
                    }
                }
            }
        }
    }

    private void SetEmissionRate(ParticleSystem particleSystem, float rate)
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = rate;
    }
}
