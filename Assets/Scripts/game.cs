using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using SmartLocalization;

public class game : MonoBehaviour {

    public string serverAddress = "127.0.0.1";
    public const int serverPort = 32211;
    public bool isConnected = false;
    private static game singleton;
    private Socket sServer;
    ErrorPopup errorHandler;
    Rect errorWindow = new Rect((Screen.width - 800) / 2, (Screen.height - 300) / 2, 800, 300);
    
    void Awake()
    {
        errorHandler = new ErrorPopup();
        gameObject.AddComponent("Dataharvester");
        gameObject.AddComponent("MainMenu");
        gameObject.AddComponent("ServerConnector");
        sServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress remoteIPAddress = IPAddress.Parse(serverAddress);
        IPEndPoint remoteEndPoint = new IPEndPoint(remoteIPAddress, serverPort);
        singleton = this;
        try
        {
            sServer.Connect(remoteEndPoint);
        }
        catch (Exception e)
        {
            Debug.Log("Connectionerror");
            errorHandler.add("Connection Failed", e.ToString());
        }
    }

	void Start () {
    }
    void OnGUI()
    {

        if (errorHandler.liste.Count > 0)
        {

            GUI.color = Color.red;
            Error err = (Error)errorHandler.liste[0];
            errorWindow = GUI.Window(0, errorWindow, DoMyWindow, err.getTitle());
        }
    }
	// Update is called once per frame
	void Update () {
        if (isConnected != sServer.Connected)
        {
            isConnected = sServer.Connected;
        }
	}
    void DoMyWindow(int windowID)
    {
        if (errorHandler.liste.Count > 0)
        {
            Error err = (Error)errorHandler.liste[0];
            GUI.TextField(new Rect(5, 25, 790, 250), err.getText());
        }
        else
        {
            GUI.TextField(new Rect(5, 25, 790, 250), "Hier stimmt was nicht, kann Fehlermeldung nicht lesen!");
        }
        if (GUI.Button(new Rect(200, 275, 100, 20), LanguageManager.Instance.GetTextValue("errorwindow_button_okay")))
        {
            errorHandler.liste.RemoveAt(0);
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
    void OnApplicationQuit()
    {
        sServer.Close();
        sServer = null;
    }
    public static void Send(MessageData msg)
    {
        if (singleton == null)
        {
            return;
        }
        Debug.Log("Sending Package...");
        byte[] sendData = MessageData.ToByteArray(msg);
        byte[] buffer = new byte[1];
        buffer[0] = (byte)sendData.Length;
        singleton.sServer.Send(buffer);
        singleton.sServer.Send(sendData);
    }
}
