using System.Collections;
using UnityEngine;

public class DeathFlickering : MonoBehaviour
{
    static public float MinFlickerTime = 0.0375f;
    static public float MaxFlickerTime = 0.075f;
    static public float FlickerTime = 1.5f;
    [SerializeField] private GameObject MiniMapFlickerObject;
    [SerializeField] private GameObject MapFlickerObject;
    
    static public DeathFlickering instance;

    /*static public void StartFlickering(DangerZone2D instance)
    {
        instance.StartCoroutine(instance.FlickerTimes());
        instance.StartCoroutine(instance.MinimapFlicker());
        instance.StartCoroutine(instance.MapFlicker());
        instance.StartCoroutine(instance.LightsFlicker());
    }*/
    void Awake()
    {
        instance = this;
    }


    public IEnumerator MinimapFlicker()
    {
        while (true)
        {
            if (MiniMapFlickerObject == null)
            {
                Debug.LogWarning("MiniMapFlickerObject is not assigned in the inspector.");
                yield break;
            }

            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            MiniMapFlickerObject.SetActive(false);
            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            MiniMapFlickerObject.SetActive(true);
        }
    }

    public IEnumerator MapFlicker()
    {
        while (true)
        {
            if (MapFlickerObject == null)
            {
                Debug.LogWarning("MapFlickerObject is not assigned in the inspector.");
                yield break;
            }

            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            MapFlickerObject.SetActive(false);
            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            MapFlickerObject.SetActive(true);
        }
    }

    public IEnumerator LightsFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            DangerZone2D.DeathActivated = true;
            yield return new WaitForSeconds(Mathf.Max(MinFlickerTime, MaxFlickerTime));
            DangerZone2D.DeathActivated = false;
        }
    }
    public void StopAllFlickering()
    {

        StopCoroutine(instance.MinimapFlicker());
        StopCoroutine(instance.MapFlicker());
        StopCoroutine(instance.LightsFlicker());

    }

    // Static methods to control coroutines from other scripts

    /*static public void StartMinimapFlicker()
    {
        if (instance != null)
            instance.StartCoroutine(instance.MinimapFlicker());
    }

    static public void StartMapFlicker()
    {
        if (instance != null)
            instance.StartCoroutine(instance.MapFlicker());
    }

    static public void StartLightsFlicker()
    {
        if (instance != null)
            instance.StartCoroutine(instance.LightsFlicker());
    }*/


}
