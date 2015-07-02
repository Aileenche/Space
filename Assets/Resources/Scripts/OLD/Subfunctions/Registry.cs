using UnityEngine;
using System.Collections;
using System;
public class Registry
{
    public static string getRegistryEntry(string key)
    {
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Reference.registryname);
        object retval;
        try
        {
            retval = retkey.GetValue(key);
        }
        catch (Exception e)
        {
            retval = "notSetYet";
        }
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
        Microsoft.Win32.RegistryKey setval = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Reference.registryname);
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
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Reference.registryname);
        object retval;
        try
        {
            retval = retkey.GetValue(key);
        }
        catch (Exception e)
        {
            retval = "notSetYet";
        }
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
        Debug.Log("Registrykey [" + key + "] returns null and has no standard Value. retval[" + retval + "]");
        return false;
    }
    public static void setupRegistry()
    {
        createIfNotExist("rememberme", "true");
        createIfNotExist("username", "Username");
        createIfNotExist("password", "");
        createIfNotExist("fullscreen", "true");
        createIfNotExist("screenwidth", "1920");
        createIfNotExist("screenheight", "1080");
        createIfNotExist("antialiasing", "8");
        createIfNotExist("vsync", "1");
        createIfNotExist("runinbackground", "true");
    }
    private static void createIfNotExist(string key, string value)
    {
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Reference.registryname);
        object retval;
        try
        {
            //Debug.Log("Fetching " +key);
            retval = retkey.GetValue(key);
            //Debug.Log("Returned " + retval);
        }
        catch (Exception e)
        {
            Debug.Log("Error " + e);
            retval = null;
        }
        if (retval == null)
        {
            setRegistryEntry(key, value);
        }
    }
}
