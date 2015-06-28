using UnityEngine;
using System.Collections;
using SmartLocalization;

public class ErrorPopup : MonoBehaviour
{
    Rect loginwindow = new Rect((Screen.width - 500) / 2, (Screen.height - 300) / 2, 500, 300);
    private static ArrayList liste;
    // Use this for initialization
    void Start()
    {
        liste = new ArrayList();
    }

    // Update is called once per frame
    public void OnGUI()
    {
        if (liste.Count > 0)
        {

            GUI.color = Color.red;
            Error err = (Error)liste[0];
            loginwindow = GUI.Window(0, loginwindow, DoMyWindow, err.getTitle());
        }
    }
    void DoMyWindow(int windowID)
    {
        if (liste.Count > 0)
        {
            Error err = (Error)liste[0];
            GUI.TextField(new Rect(5, 25, 490, 250), err.getText());
        }
        else
        {
            GUI.TextField(new Rect(5, 25, 490, 250), "Hier stimmt was nicht, kann Fehlermeldung nicht lesen!");
        }
        if (GUI.Button(new Rect(200, 275, 100, 20), LanguageManager.Instance.GetTextValue("errorwindow_button_okay")))
        {
            liste.RemoveAt(0);
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    internal static void add(string title, string text)
    {
        Error err = new Error(title, text);
        liste.Add(err);
    }
}
