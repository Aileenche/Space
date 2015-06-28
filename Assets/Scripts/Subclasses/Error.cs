using UnityEngine;
using System.Collections;

public class Error {
    private string title;
    private string text;

    public Error(string title, string text)
    {
        this.title = title;
        this.text = text;
    }
    public string getTitle()
    {
        return this.title;
    }
    public string getText()
    {
        return this.text;
    }
}
