using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MetalAmbient : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips = new AudioClip[3];
    [Min(0.1f)]
    [SerializeField] private float intervalSeconds = 180f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void OnEnable()
    {
        StartCoroutine(PlayMetalAmbient());
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator PlayMetalAmbient()
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("Assign metal ambient clips to the Clips", this);
            yield break;
        }

        while (true)
        {
            // Waiting the interval
            yield return new WaitForSeconds(intervalSeconds);

            // Choosing a random clip
            int index = Random.Range(0, clips.Length);
            AudioClip clip = clips[index];

            if (clip == null)
            {
                continue;
            }

            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the sound finishes before starting the next interval
            while (audioSource.isPlaying)
            {
                yield return null;

            }
        }
    }
}
