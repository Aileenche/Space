using UnityEngine;
using System.Collections;
public class Registry
{
    public static string getRegistryEntry(string key)
    {
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Space!");
        object retval = retkey.GetValue(key);
        if (retval == null)
        {
            object standardKey = hasStandardValue(key);
            if (standardKey != "ThereisnoKeyforThat!")
            {
                setRegistryEntry(key, standardKey);
                return standardKey.ToString();
            }
            return "Registrykey [" + key + "] returns null and has no standard Value";
        }
        return retval.ToString();
    }
    public static bool setRegistryEntry(string key, object value)
    {
        Microsoft.Win32.RegistryKey setval = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Space!");
        setval.SetValue(key, value);
        setval.Close();
        return false;
    }
    private static object hasStandardValue(string key)
    {
        switch (key)
        {
            case ("RememberMe"): return false;
            case ("FPSCounter"): return false;
        }
        return "ThereisnoKeyforThat!";
    }

    internal static bool getRegistryBool(string key)
    {
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Space!");
        object retval = retkey.GetValue(key);
        object standardKey = "notSetYet";
        if (retval == null)
        {
            standardKey = hasStandardValue(key);
            if (standardKey != "ThereisnoKeyforThat!")
            {
                setRegistryEntry(key, standardKey);
                if (standardKey.ToString().ToLower() == "true")
                {
                    return true;
                }
                if (standardKey.ToString().ToLower() == "false")
                {
                    return false;
                }
            } 
            Debug.Log("Registrykey [" + key + "] returns null and has no standard Value. retval[" + retval + "] standardvalue[" + standardKey + "]");
            return false;
        }
        if (retval.ToString().ToLower() == "true")
        {
            return true;
        }
        if (retval.ToString().ToLower() == "false")
        {
            return false;
        }
        Debug.Log("Registrykey [" + key + "] returns null and has no standard Value. retval["+retval+"]");
        return false;
    }
}
