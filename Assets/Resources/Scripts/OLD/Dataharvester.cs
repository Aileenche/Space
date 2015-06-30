using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dataharvester : MonoBehaviour
{
    public static Setting FPSCounter;
    public static Setting RememberMe;
    public static Setting Username;
    public static Setting Password;
    public static Setting isOnline;
    public static Setting Language;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    public void Start()
    {
        FPSCounter = new Setting("FPSCounter", Registry.getRegistryBool("FPSCounter"));
        RememberMe = new Setting("RememberMe", Registry.getRegistryBool("RememberMe"));
        Username = new Setting("Username", Registry.getRegistryEntry("Username"));
        Password = new Setting("Password", Registry.getRegistryEntry("Password"));
        isOnline = new Setting("isOnline", false);
        Language = new Setting("Language", Registry.getRegistryEntry("Language"));
    }
    public static void set(object key, object Value)
    {
        Registry.setRegistryEntry(key.ToString(), Value.ToString());
    }
}
