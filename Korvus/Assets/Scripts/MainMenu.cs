using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
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
        }
    }
}
