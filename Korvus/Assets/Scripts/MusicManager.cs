using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //take a clip and adds a .05 second fade in and fade out to it
            IEnumerator AnimateMusicCrossFade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent <1)
        {
            percent += Time.deltaTime * (1 / fadeDuration);
            AudioListener.volume = Mathf.Lerp(1, 0, percent);
            yield return null;
        }
        
            musicSource.clip= nextTrack;
            musicSource.Play();
            percent = 0;
            while (percent < 1)
        {
            percent += Time.deltaTime * (1 / fadeDuration);
            AudioListener.volume = Mathf.Lerp(0, 1, percent);
            yield return null;
        }
    }
    }
        
    
