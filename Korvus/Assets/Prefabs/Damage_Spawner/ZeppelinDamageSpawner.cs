using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ZeppelinDamageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject damagedPart;
    [SerializeField, Min(0.1f)] private float spawnInterval = 5f;

    private void OnEnable()
    {
        if (damagedPart == null)
        {
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Max(0.1f, spawnInterval));

            if (damagedPart == null)
                yield break;

            // All direct children are spawn points.
            int count = transform.childCount;
            if (count == 0)
                continue;

            // Automatically selects a random spawn point
            int index = Random.Range(0, count);
            Transform spawner = transform.GetChild(index);

            // This copies location and rotation of the spawner
            GameObject instance = Instantiate(damagedPart, spawner.position, spawner.rotation);
            instance.name = "DamagedPart";
        }
    }
}
