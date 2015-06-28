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
            if (GUI.Button(new Rect(30, Screen.height - 70, 100, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_settings")))
            {
                menulevel = 1;
            }
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_exit")))
            {
                Application.Quit();


            }
        }
        else if (menulevel == 1) //SETTINGS
        {
            if (GUI.Button(new Rect(30, Screen.height - 175, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fullscreen")))
            {
                if (Screen.fullScreen)
                {
                    Screen.fullScreen = false;
                }
                else
                {
                    Screen.SetResolution(1920, 1080, true);
                }
            }
            //FPSCounter
            if (Dataharvester.get("FPSCounter").ToString().ToLower() == "true")
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpsoff")))
                {
                    Dataharvester.set("FPSCounter", false);
                }
            }
            else
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpson")))
                {
                    Dataharvester.set("FPSCounter", true);
                }
            }
            //Quality
            int quality = QualitySettings.GetQualityLevel();

            if (quality > 0)
            {
                if (GUI.Button(new Rect(30, Screen.height - 105, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_lowerdetails")))
                {
                    QualitySettings.DecreaseLevel();
                }
            }

            if (quality < 5)
            {
                if (GUI.Button(new Rect(30, Screen.height - 70, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_higherdetails")))
                {
                    QualitySettings.IncreaseLevel();
                }
            }

            GUI.Label(new Rect(30, Screen.height - 75, 100, 20), Functions.getLocalizedQualityLevel());


            //Back
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            {
                menulevel = 0;
            }
        }
    }

}
