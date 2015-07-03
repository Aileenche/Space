using UnityEngine;
using System.Collections;
using SmartLocalization;

public class MainMenu
{

    // Use this for initialization
    private int menulevel = 0;
    public static string news = "";
    public void Start()
    {
        //LanguageManager.Instance.ChangeLanguage(Dataharvester.Language.getString());
    }

    public static void close_game()
    {
        Application.Quit();
    }
    public void OnGUI()
    {
        GUI.skin = Main.singleton.skin;

        GUI.color = Color.cyan;
        if (menulevel == 0)
        {
            if (GUI.Button(new Rect(30, Screen.height - 105, 250, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_connect")))
            {
                menulevel = 2;
            }
            if (GUI.Button(new Rect(30, Screen.height - 70, 250, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_settings")))
            {
                menulevel = 1;
            }
            if (GUI.Button(new Rect(30, Screen.height - 35, 250, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_exit")))
            {
                Application.Quit();
            }

            GUI.BeginGroup(new Rect((Screen.width - 250) , 0, 250, Screen.height), "", "Box");
            if (GUI.Button(new Rect(0, 0, 250, 30), "Refresh"))
            {
                Main.networkClient.send(new PacketConnector.Packet(PacketConnector.PacketType.getNews, Main.networkClient.ID));
            }
            GUI.color = Color.white;
            GUI.Label(new Rect(0, 30, 250, Screen.height), ""+news);
            GUI.color = Color.cyan;
            GUI.EndGroup();
        }
        else if (menulevel == 1) //SETTINGS
        {
            if (GUI.Button(new Rect(30, Screen.height - 210, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_language")))
            {
                if (LanguageManager.Instance.CurrentlyLoadedCulture.languageCode == "de-DE")
                {
                    LanguageManager.Instance.ChangeLanguage("en-US");
                }
                else
                {
                    LanguageManager.Instance.ChangeLanguage("de-DE");
                }
            }
            if (GUI.Button(new Rect(30, Screen.height - 175, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fullscreen")))
            {
                //if (Screen.fullScreen)
                //{
                //    Screen.fullScreen = false;
                //}
                //else
                //{
                //    Screen.SetResolution(1920, 1080, true);
                //}
            }
            //FPSCounter
            //if (Dataharvester.FPSCounter.getBool())
            //{
            //    if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpsoff")))
            //    {
            //        Dataharvester.FPSCounter.set(false);
            //    }
            //}
            //else
            //{
            //    if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpson")))
            //    {
            //        Dataharvester.FPSCounter.set(true);
            //    }
            //}
            //Quality
            int quality = QualitySettings.GetQualityLevel();

            if (quality > 0)
            {
                if (GUI.Button(new Rect(30, Screen.height - 105, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_lowerdetails")))
                {
                    //QualitySettings.DecreaseLevel();
                }
            }

            if (quality < 5)
            {
                if (GUI.Button(new Rect(30, Screen.height - 70, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_higherdetails")))
                {
                    //QualitySettings.IncreaseLevel();
                }
            }

            // GUI.Label(new Rect(30, Screen.height - 75, 100, 20), Functions.getLocalizedQualityLevel());


            //Back
            if (GUI.Button(new Rect(30, Screen.height - 35, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            {
                menulevel = 0;
            }
        }
        else if (menulevel == 2)
        {
            //GUI.BeginGroup(new Rect((Screen.width - 500) / 2, (Screen.height - 300) / 2, 500, 300), "", "Box");
            //string Username = GUI.TextField(new Rect(0, 0, 200, 28), Dataharvester.Username.getString());
            //Dataharvester.Username.set(Username);
            //string Password = GUI.PasswordField(new Rect(0, 55, 200, 28), Dataharvester.Password.getString(), char.Parse("*"));
            //Dataharvester.Password.set(Password);
            //bool RememberMe = GUI.Toggle(new Rect(0, 110, 200, 50), Dataharvester.RememberMe.getBool(), "Rememberme");
            //Dataharvester.RememberMe.set(RememberMe);
            //if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            //{
            //    menulevel = 0;
            //}
            //GUI.EndGroup();
            //if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            //{
            //    saveUserCredentials(RememberMe, Username, Password);
            //    menulevel = 0;
            //}

        }
    }

    private void saveUserCredentials(bool RememberMe, string Username, string Password)
    {
        //    if (RememberMe)
        //    {
        //        Dataharvester.Username.set(Username);
        //        Dataharvester.Password.set(Password);
        //        Dataharvester.RememberMe.set(RememberMe);
        //        Dataharvester.set("Username", Username);
        //        Dataharvester.set("Password", Password);
        //        Dataharvester.set("RememberMe", RememberMe);
        //    }
        //    else
        //    {
        //        Dataharvester.Username.set("Username");
        //        Dataharvester.Password.set("");
        //        Dataharvester.RememberMe.set(RememberMe);
        //        Dataharvester.set("Username", "Username");
        //        Dataharvester.set("Password", "");
        //        Dataharvester.set("RememberMe", "RememberMe");
        //    }
    }
}
