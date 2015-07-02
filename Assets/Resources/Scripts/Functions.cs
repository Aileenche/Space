using UnityEngine;
using System.Collections;
using System;

public class Functions
{
    public static void setupClientQuality()
    {
        QualitySettings.vSyncCount = Int32.Parse(Registry.getRegistryEntry("vsync"));
        Screen.SetResolution(Int32.Parse(Registry.getRegistryEntry("screenwidth")), Int32.Parse(Registry.getRegistryEntry("screenheight")), Registry.getRegistryBool("fullscreen"));
        QualitySettings.antiAliasing = Int32.Parse(Registry.getRegistryEntry("antialiasing"));
        Application.runInBackground = Registry.getRegistryBool("runinbackground");
    }
}
