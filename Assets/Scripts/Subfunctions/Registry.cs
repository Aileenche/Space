using UnityEngine;
using System.Collections;
public class Registry : MonoBehaviour
{
    public static string getRegistryEntry(string key)
    {
        Microsoft.Win32.RegistryKey retkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Space!");
        object retval = retkey.GetValue(key);
        if (retval == null)
        {
            string standardKey = hasStandardValue(key);
            if (standardKey != "ThereisnoKeyforThat!")
            {
                setRegistryEntry(key, standardKey);
                return standardKey;
            }
            return "Registrykey [" + key + "] returns null and has no standard Value";
        }
        return retval.ToString();
    }
    public static bool setRegistryEntry(string key, string value)
    {
        Microsoft.Win32.RegistryKey setval = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Space!");
        setval.SetValue(key, value);
        setval.Close();
        return false;
    }
    private static string hasStandardValue(string key)
    {
        switch (key)
        {
            case ("RememberMe"): return "false";
            case ("Language"): return "de";
            case ("LogMeIn"): return "false";
            case ("FPSCounter"): return "false";
        }
        return "ThereisnoKeyforThat!";
    }
}
