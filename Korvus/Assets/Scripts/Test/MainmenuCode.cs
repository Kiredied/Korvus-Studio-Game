using UnityEngine;
using UnityEngine.UI;

public class MainmenuCode : MonoBehaviour
{

    static public bool isCamReset = false;
    [SerializeField] private GameObject PlayMenuButtonHider;
    [SerializeField] private GameObject CamResetDialogue;
    static public bool TurnDarkBool = false;
    [SerializeField] GameObject DarknessMainMenu;
    Image Image;

    bool mainmenulog = false;

    void Start()
    {
        PlayMenuButtonHider.SetActive(true);
        CamResetDialogue.SetActive(true);

        Image = DarknessMainMenu.GetComponentInChildren<Image>();
    }

    public void CamResetConfirmed()
    {
        isCamReset = true;
        PlayMenuButtonHider.SetActive(false);
        CamResetDialogue.SetActive(false);

    }
/*
    public void TurnDark()
    {
        TurnDarkBool = true;

    }
*/

    public void Update()
    {
        if (TurnDarkBool)
        {
            if (!mainmenulog) {Debug.Log("Darkness. (Post-Processing)");  mainmenulog = true; }
            if (DarknessMainMenu != null)
            {

                if (Image != null)
                {
                    Image.color = new Color(0, 0, 0, ((Image.color.a * 255f) * 30 + 255f) / 31f / 255f);
                }
            } 
        }

/*
        Debug.Log("Image.color.a: " + Image.color.a);
        Debug.Log("(Image.color.a * 255f + 255f)/2 " + (Image.color.a * 255f + 255f)/2);
        Debug.Log("Image.color" + Image.color);
        // for debug purposes
        if (Image != null)
        {
            Image.color = new Color(0, 0, 0, ((Image.color.a * 255f)*30 + 255f) / 31f / 255f);
        }
        */
        

    }
}
