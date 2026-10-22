using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play button is pressed, the game scene will load
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    // After the Quit button is pressed, the application will close
    public void Quit()
    {
        Application.Quit();
    }
}
