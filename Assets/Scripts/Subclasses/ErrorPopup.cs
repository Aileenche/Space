using UnityEngine;
using System.Collections;
using SmartLocalization;

public class ErrorPopup
{
    public  ArrayList liste = new ArrayList();

    public void add(string title, string text)
    {
        Error err = new Error(title, text);
        liste.Add(err);
    }
}
