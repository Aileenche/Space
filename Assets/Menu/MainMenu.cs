using UnityEngine;
using System.Collections;
using SmartLocalization;

public class MainMenu : MonoBehaviour
{
    // Use this for initialization
    private int menulevel;
    void Start()
    {
        Functions.tryLogin();
        menulevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime);
        
    }

    void OnGUI()
    {
        if (menulevel == 0)
        {
            if (GUI.Button(new
             Rect(30, Screen.height - 105, 100, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_connect")))
            {
                LanguageManager.Instance.ChangeLanguage("en-US");
            }
            if (GUI.Button(new Rect(30, Screen.height - 70, 100, 25), "Settings"))
            {
                menulevel = 1;
            }
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), "Quit"))
            {
                Application.Quit();


            }
        }
        else if (menulevel == 1) //SETTINGS
        {
            //FPSCounter

            if (Dataharvester.get("FPSCounter").ToString().ToLower() == "true")
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), "Toggle FPS Counter Off"))
                {
                    Dataharvester.set("FPSCounter", false);
                }
            }
            else
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), "Toggle FPS Counter On"))
                {
                    Dataharvester.set("FPSCounter", true);
                }
            }
            //Quality
            int quality = QualitySettings.GetQualityLevel();

            if (quality > 0)
            {
                if (GUI.Button(new Rect(30, Screen.height - 105, 250, 25), "Set Details Lower"))
                {
                    QualitySettings.DecreaseLevel();
                }
            }

            if (quality < 5)
            {
                if (GUI.Button(new Rect(30, Screen.height - 70, 250, 25), "Set Details Higher"))
                {
                    QualitySettings.IncreaseLevel();
                }
            }

            GUI.Label(new Rect(30, Screen.height - 75, 100, 20), Functions.getLocalizedQualityLevel());


            //Back
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), "Back"))
            {
                menulevel = 0;
            }
        }
    }

}
