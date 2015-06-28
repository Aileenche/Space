using UnityEngine;
using System.Collections;

public class Setting
{
    private string name;
    private object value;

    public Setting(string name, object value)
    {
        this.name = name;
        this.value = value;
    }
    public object getObject()
    {
        return this.value;
    }
    public bool getBool()
    {
        if (this.value.ToString().ToLower() == "true")
        {
            return true;
        }
        return false;
    }
    public string getString()
    {
        return this.value.ToString();
    }
    public void set(object value)
    {
        this.value = value;
    }
}
