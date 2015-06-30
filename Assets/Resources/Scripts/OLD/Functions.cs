using UnityEngine;
using System.Collections;
using System;

public class Functions : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static string getLocalizedQualityLevel()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case (0): return "Fastest";
            case (1): return "Fast";
            case (2): return "Simple";
            case (3): return "Good";
            case (4): return "Beautiful";
            case (5): return "Fantastic";

        }
        return "Error";
    }

    internal static void tryLogin()
    {
        try
        {

        }
        catch (Exception e)
        {
            throw new System.NotImplementedException();
        }
    }
}
