using UnityEngine;
using System.Collections;
using System.Threading;

public class ServerConnector : MonoBehaviour
{

    private bool showLoginWindow;
    public ServerConnector()
    {
        // TODO: Complete member initialization
    }
    public void initialize()
    {
        if (Dataharvester.RememberMe.getBool())
        {
            if (Dataharvester.LogMeIn.getBool())
            {
                showLogin(true, true, Dataharvester.Username.getString(), Dataharvester.Password.getString());
            }
            else
            {
                showLogin(true, false, Dataharvester.Username.getString(), Dataharvester.Password.getString());
            }
        }
        else
        {
            showLogin(false, false, "", "");
        }
    }

    void OnGUI()
    {
        //Debug.Log(""+this.showLoginWindow);
        if (!Dataharvester.isOnline.getBool())
        {
        }
    }
    public void showLogin(bool rememberme, bool logmein, object username, object password)
    {
        Debug.Log("Called");
        Debug.Log("bevor" + showLoginWindow);
        showLoginWindow = true;
        Debug.Log("danach" + showLoginWindow);
    }

}
