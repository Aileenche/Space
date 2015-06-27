using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dataharvester : MonoBehaviour
{

    private static Dictionary<string, object> dataharv = new Dictionary<string, object>();


    // Use this for initialization
    void Start()
    {
        dataharv.Add("FPSCounter", Registry.getRegistryEntry("FPSCounter"));
        dataharv.Add("Language", Registry.getRegistryEntry("Language"));
        dataharv.Add("RememberMe", Registry.getRegistryEntry("RememberMe"));
        dataharv.Add("LogMeIn", Registry.getRegistryEntry("LogMeIn"));
        dataharv.Add("Username", Registry.getRegistryEntry("Username"));
        dataharv.Add("Password", Registry.getRegistryEntry("Password"));
    }
    public static bool set(string key, object Value)
    {
        if (dataharv.ContainsKey(key))
        {
            dataharv.Remove(key);
            dataharv.Add(key, Value);
            Registry.setRegistryEntry(key,Value.ToString());
        }
        else
        {
            dataharv.Add(key, Value);
            Registry.setRegistryEntry(key, Value.ToString());
        }
        return false;
    }
    public static object get(string key)
    {

        if (dataharv.ContainsKey(key))
        {
            return dataharv[key];
        }
        Debug.Log("Dataharvester was queried with nonexisting [" + key + "]");
        return false;
    }
}
