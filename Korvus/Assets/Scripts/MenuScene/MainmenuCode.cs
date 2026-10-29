using UnityEngine;
using UnityEngine.UI;

public class MainmenuCode : MonoBehaviour
{
    static public bool isCamReset = false;
    [SerializeField] private GameObject PlayMenuButtonHider;
    [SerializeField] private GameObject CamResetDialogue;
    static public bool TurnDarkBool = false;
    [SerializeField] GameObject DarknessMainMenu;
    [SerializeField] Material DarknessMaterialTransparent;
    [SerializeField] Material DarknessMaterialOpaque;

    Color darknessColor;
    bool mainmenulog = false;
    private int time = 0;

    void Start()
    {
        PlayMenuButtonHider.SetActive(true);
        CamResetDialogue.SetActive(true);

        darknessColor = new Color(0, 0, 0, 0);
        DarknessMainMenu.GetComponent<MeshRenderer>().material = DarknessMaterialTransparent;
        DarknessMainMenu.GetComponent<MeshRenderer>().material.color = darknessColor;
    }

    public void CamResetConfirmed()
    {
        isCamReset = true;
        PlayMenuButtonHider.SetActive(false);
        CamResetDialogue.SetActive(false);
    }

    public void Update()
    {
        if (TurnDarkBool)
        {
            if (!mainmenulog) { Debug.Log("Darkness. (Post-Processing)"); mainmenulog = true; }

            if (DarknessMainMenu != null)
            {
                time += 1;

                darknessColor.a = ((darknessColor.a * 255f) * 30f + 255f) / 31f / 255f;
                DarknessMainMenu.GetComponent<MeshRenderer>().material.color = darknessColor;

                if (time > 150)
                {
                    DarknessMainMenu.GetComponent<MeshRenderer>().material = DarknessMaterialOpaque;
                }
            }
        }
    }
}