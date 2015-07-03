using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SmartLocalization;

public class Menu : MonoBehaviour
{
    public GameObject AntiAliasingText;
    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject[] mainmenu;
    public GameObject[] settings;


    private void Start()
    {
        mainmenu[0].transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("mainmenu_button_connect");
        mainmenu[1].transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("mainmenu_button_settings");
        mainmenu[2].transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("mainmenu_button_exit");

        settings[0].transform.GetChild(0).GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("settingsmenu_button_back");

        //Set Anti Aliasin
        AntiAliasingText.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("settingmenu_button_antialiasing") + ": " + Registry.getRegistryEntry("antialiasing");
        QualitySettings.antiAliasing = Registry.getRegistryInt("antialiasing");
    }

    public void OnButtonClicked(string name)
    {
        switch (name)
        {
            case ("Connect"):

                break;
            case ("Settings"):
                MainMenu.SetActive(false);
                Settings.SetActive(true);
                break;
            case ("Exit"):
                Application.Quit();
                break;
            case ("Back"):
                MainMenu.SetActive(true);
                Settings.SetActive(false);
                break;
            default:
                Debug.Log("This is not the button you are looking for...");
                break;
        }
    }

    public void ChangeAA(int value)
    {
        AntiAliasingText.GetComponent<Text>().text = LanguageManager.Instance.GetTextValue("settingmenu_button_antialiasing") + ": " + value;
        QualitySettings.antiAliasing = value;
        Registry.setRegistryEntry("antialiasing", value);
    }
}