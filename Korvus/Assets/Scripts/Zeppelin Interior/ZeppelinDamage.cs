using System.Collections.Generic;
using UnityEngine;

using UnityRandom = UnityEngine.Random;

public class ZeppelinDamage : MonoBehaviour
{
    static public float minDamageInterval = 20f;
    static public float maxDamageInterval = 60f;
    [SerializeField] private GameObject damageLocationParent;

    public static int DamageCount { get; set; }
    public static float DamagePercentage { get; private set; }

    private float damageTimer;
    private float nextDamageTime;

    private void Start()
    {
        if (maxDamageInterval < minDamageInterval)
        {
            maxDamageInterval = minDamageInterval;
        }

        DamageCount = 0;
        DamagePercentage = 0f;
        damageTimer = 0f;
        nextDamageTime = GetNextDamageTime();
    }

    private void FixedUpdate()
    {
        var killed = false;
        if (DamagePercentage >= 100f)
        {
            if (!killed)
            {
                killed = true;
                DangerZone2D.MountainDeath = true;
            }
        }
        if (damageLocationParent == null)
        {
            return;
        }

        damageTimer += Time.fixedDeltaTime;

        if (damageTimer < nextDamageTime)
        {
            return;
        }

        damageTimer = 0f;
        nextDamageTime = GetNextDamageTime();
        DoDamage();

        if (DangerZone2D.DangerZone || !DangerZone2D.DangerWall)
        {
            minDamageInterval = 20f;
            maxDamageInterval = 35f;
        }
        else
        {
            if (DangerZone2D.PreviousDangerZone)
            {
                minDamageInterval = 30f;
                maxDamageInterval = 50f;
            }
            else
            {
                if (DangerZone2D.DangerWall)
                {
                    minDamageInterval = 15f;
                    maxDamageInterval = 25f;
                }
                else
                {
                    if (DangerZone2D.DeadZone)
                    {
                        minDamageInterval = 2f;
                        maxDamageInterval = 5f;
                    }
                    else
                    {
                        minDamageInterval = 45f;
                        maxDamageInterval = 70f;
                    }
                }
            }
        }
    }

    private float GetNextDamageTime()
    {
        return UnityRandom.Range(minDamageInterval, maxDamageInterval);
    }

    private void DoDamage()
    {
        IndividualDamage[] damageLocations =
            damageLocationParent.GetComponentsInChildren<IndividualDamage>(true);

        var availableLocations = new List<IndividualDamage>();

        foreach (IndividualDamage damageLocation in damageLocations)
        {
            if (!damageLocation.objectdamaged)
            {
                availableLocations.Add(damageLocation);
            }
        }

        if (availableLocations.Count == 0)
        {
            Debug.Log("No available damage locations.");
            return;
        }

        int index = UnityRandom.Range(0, availableLocations.Count);
        IndividualDamage selectedLocation = availableLocations[index];

        // Get the DamageLocationX parent
        GameObject damageRoot = selectedLocation.gameObject;

        // Ensure the parent gameobject is active
        damageRoot.SetActive(true);

        // Find particle system as a child of DamageLocationX
        ParticleSystem ps = damageRoot.GetComponentInChildren<ParticleSystem>(true);

        if (ps != null)
        {
            ps.gameObject.SetActive(true);
            Debug.Log("Particle system " + ps.gameObject.name + " activated under " + damageRoot.name);
        }
        else
        {
            Debug.LogWarning("No particle system found under " + damageRoot.name);
        }

        // Enable colliders for the repair zone
        Collider[] colliders = damageRoot.GetComponentsInChildren<Collider>(true);
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        selectedLocation.objectdamaged = true;

        UpdateDamageStatistics(damageLocations);

        Debug.Log("Activated damage location: " + damageRoot.name);
    }

    public static void RegisterRepair(IndividualDamage repairedLocation)
    {
        if (repairedLocation != null)
        {
            repairedLocation.objectdamaged = false;
        }
    }

    public static void UpdateDamageStatistics(IndividualDamage[] damageLocations)
    {
        DamageCount = 0;

        foreach (IndividualDamage damageLocation in damageLocations)
        {
            if (damageLocation.objectdamaged)
            {
                DamageCount++;
            }
        }

        DamagePercentage = damageLocations.Length == 0
            ? 0f
            : Mathf.Clamp01((float)DamageCount / damageLocations.Length) * 100f;
    }
}

