using UnityEngine;
using System.Collections;
using SmartLocalization;

public class MainMenu : MonoBehaviour
{
    // Use this for initialization
    private int menulevel;
    private MessageData msg = new MessageData();
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
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
        GUI.color = Color.cyan;
        if (menulevel == 0)
        {
            if (GUI.Button(new
             Rect(30, Screen.height - 105, 100, 25), LanguageManager.Instance.GetTextValue("mainmenu_button_connect")))
            {
                //LanguageManager.Instance.ChangeLanguage("en-US");
                msg.type = 10;
                msg.stringData = "Hallo Eddy";
                game.Send(msg);
                menulevel = 2;
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
            if (GUI.Button(new Rect(30, Screen.height - 175, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fullscreen")))
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
            if (Dataharvester.FPSCounter.getBool())
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpsoff")))
                {
                    Dataharvester.FPSCounter.set(false);
                }
            }
            else
            {
                if (GUI.Button(new Rect(30, Screen.height - 140, 250, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_fpson")))
                {
                    Dataharvester.FPSCounter.set(true);
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
        else if (menulevel == 2)
        {
            GUI.BeginGroup(new Rect((Screen.width - 500) / 2, (Screen.height - 300) / 2, 500, 300), "", "Box");
            string Username = GUI.TextField(new Rect(0, 0, 200, 28), Dataharvester.Username.getString());
            Dataharvester.Username.set(Username);
            string Password = GUI.PasswordField(new Rect(0, 55, 200, 28), Dataharvester.Password.getString(), char.Parse("*"));
            Dataharvester.Password.set(Password);
            bool RememberMe = GUI.Toggle(new Rect(0, 110, 200, 50), Dataharvester.RememberMe.getBool(), "Rememberme");
            Dataharvester.RememberMe.set(RememberMe);
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            {
                menulevel = 0;
            }
            GUI.EndGroup();
            if (GUI.Button(new Rect(30, Screen.height - 35, 100, 25), LanguageManager.Instance.GetTextValue("settingsmenu_button_back")))
            {
                saveUserCredentials(RememberMe,Username,Password);
                menulevel = 0;
            }

        }
    }

    private void saveUserCredentials(bool RememberMe, string Username, string Password)
    {
        if (RememberMe)
        {
            Dataharvester.Username.set(Username);
            Dataharvester.Password.set(Password);
            Dataharvester.RememberMe.set(RememberMe);
            Dataharvester.set("Username", Username);
            Dataharvester.set("Password", Password);
            Dataharvester.set("RememberMe", RememberMe);
        }
        else
        {
            Dataharvester.Username.set("Username");
            Dataharvester.Password.set("");
            Dataharvester.RememberMe.set(RememberMe);
            Dataharvester.set("Username", "Username");
            Dataharvester.set("Password", "");
            Dataharvester.set("RememberMe", "RememberMe");
        }
    }
}
