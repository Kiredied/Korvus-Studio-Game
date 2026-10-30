using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
        LoadVolume();
       //plays main menu music when the scene is loaded
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    
    private bool scenechanged = false;
    // Play button is pressed, the game scene will load
    public void Play()
    {
        MainmenuCode.TurnDarkBool = true;
        StartCoroutine(SpawnLoop());
    }

    // After the Quit button is pressed, the application will close
    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator SpawnLoop()
    {
        // Wait about 250 frames, giving control back to Unity each frame
        float time = 0f;
        while (time <= 250f)
        {
            time += 1f;
            yield return null;
        }

        if (!scenechanged)
        {
            scenechanged = true;
            SceneManager.LoadScene("Game");
            MusicManager.Instance.PlayMusic("Game");
        }
    }
     public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
    public void LoadVolume()
    {
        // errors out so i commented it -Diederik
        
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
