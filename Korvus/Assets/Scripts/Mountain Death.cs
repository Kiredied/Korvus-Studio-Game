using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MountainDeath : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject.CompareTag("Mountain"))
        {
            if (collision.gameObject.tag == "2DPlayer")
            {
                SceneManager.LoadScene("DeathScreen");
            }
        }
    }

    public void LoadNextInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
