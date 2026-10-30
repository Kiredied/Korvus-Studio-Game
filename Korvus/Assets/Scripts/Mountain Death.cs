using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainDeath : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Mountain")
        {
        SceneManager.LoadScene("DeathScreen");
        }
    }

    public void LoadNextInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
