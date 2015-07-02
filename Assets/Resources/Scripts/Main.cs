using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Main : MonoBehaviour
{

    public static ErrorPopup errorHandler = new ErrorPopup();
    private Rect errorWindow = new Rect((Screen.width - 800) / 2, (Screen.height - 300) / 2, 800, 300);
    public static Client networkClient = new Client();
    public static MainMenu menu = new MainMenu();

    void Start()
    {
        Registry.setupRegistry();
        Functions.setupClientQuality();
        networkClient.Start();
        Main.networkClient.send(new PacketConnector.Packet(PacketConnector.PacketType.getNews, Main.networkClient.ID));
    }

    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime);
    }

    void OnGUI()
    {
        if (errorHandler.liste.Count > 0)
        {
            GUI.color = Color.red;
            Error err = (Error)errorHandler.liste[0];
            errorWindow = GUI.Window(0, errorWindow, DoMyWindow, err.getTitle());
        }
        //menu.OnGUI();
    }
    void DoMyWindow(int windowID)
    {
        if (errorHandler.liste.Count > 0)
        {
            Error err = (Error)errorHandler.liste[0];
            GUI.TextField(new Rect(5, 25, 790, 250), err.getText());
        }
        else
        {
            GUI.TextArea(new Rect(5, 25, 790, 250), "Hier stimmt was nicht, kann Fehlermeldung nicht lesen!");
        }
        if (GUI.Button(new Rect(350, 275, 100, 20), LanguageManager.Instance.GetTextValue("errorwindow_button_okay")))
        {
            errorHandler.liste.RemoveAt(0);
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
    void OnApplicationQuit()
    {
        networkClient.CloseConnection();
    }

    public void PrintMessage(string message)
    {
        errorHandler.add("Button Error: ", message);
    }
}
