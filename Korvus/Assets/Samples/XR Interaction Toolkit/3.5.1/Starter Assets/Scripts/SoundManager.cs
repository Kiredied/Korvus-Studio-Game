using UnityEngine;

public class SoundManager : MonoBehaviour

{
    // which allows us to access the SoundManager from everywhere in the project
    public static SoundManager Instance;
    [SerializeField] private AudioSource sfx2DSource;
    [SerializeField] private SoundLibrary sfxLibrary;
    
    //Which makes sure there is only one instance of this class in the scene.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        //if clip exists, play the clip at position in the 3D space.
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }
    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
}
