using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LanguageHarvester : MonoBehaviour
{
    private static Dictionary<string, string> de = new Dictionary<string, string>();
    private static Dictionary<string, string> en = new Dictionary<string, string>();
    void Start()
    {
        var filePath = System.IO.Path.Combine(Application.dataPath, "Languagefiles\\lang_en.ini");
        var array = File.ReadAllLines(filePath);
        for (var i = 0; i < array.Length; i++)
        {
            string[] splitted = array[i].Split('|');
            en.Add(splitted[0], splitted[1]);
        }
        filePath = System.IO.Path.Combine(Application.dataPath, "Languagefiles\\lang_de.ini");
        array = File.ReadAllLines(filePath);
        for (var i = 0; i < array.Length; i++)
        {
            string[] splitted = array[i].Split('|');
            de.Add(splitted[0], splitted[1]);
        }
    }
    public static string get(string key)
    {
        switch (Dataharvester.get("Language").ToString().ToLower())
        {
            case ("de"):
                if (de.ContainsKey(key)) { return de[key]; }
                else
                {
                    learn(key);
                    return de[key];
                }
                break;
            case ("en"):
                if (en.ContainsKey(key)) { return en[key]; }
                else
                {
                    learn(key);
                    return en[key];
                }
                break;
        }
        return "NO LANGUAGE FOUND!";
    }

    private static void learn(string key)
    {
        GUI.Window(0, new Rect((Screen.width - 900) / 2, (Screen.height - 300) / 2, 900, 300), ProcessWindow, key);
    }
    private static void ProcessWindow(int windowID)
    {
        GUI.Label(new Rect(10, 20, 100, 20), "Hello World");
        if (GUI.Button(new Rect(10, 40, 100, 20), "Hello World"))
        {
            
        }
    }
}
