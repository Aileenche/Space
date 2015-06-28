using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dataharvester : MonoBehaviour
{
    public static Setting FPSCounter;
    public static Setting RememberMe;
    public static Setting LogMeIn;
    public static Setting Username;
    public static Setting Password;
    public static Setting isOnline;


    // Use this for initialization
    public void Start()
    {
        FPSCounter = new Setting("FPSCounter", Registry.getRegistryBool("FPSCounter"));
        RememberMe = new Setting("RememberMe", Registry.getRegistryBool("RememberMe"));
        LogMeIn = new Setting("LogMeIn", Registry.getRegistryBool("LogMeIn"));
        Username = new Setting("Username", Registry.getRegistryBool("Username"));
        Password = new Setting("Password", Registry.getRegistryBool("Password"));
        isOnline = new Setting("Password", false);
    }
    public static void set(object key, object Value)
    {
        Registry.setRegistryEntry(key.ToString(), Value.ToString());
    }
}
