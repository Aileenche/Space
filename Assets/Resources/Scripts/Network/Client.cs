using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using PacketConnector;
using SmartLocalization;

public class Client : MonoBehaviour
{
    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public string ID = "40";
    public string login = "Aileen";
    ErrorPopup errorHandler;
    Rect errorWindow = new Rect((Screen.width - 800) / 2, (Screen.height - 300) / 2, 800, 300);

    private bool isConnected = false;
    private Socket socket;
    private Thread thread;
    private IPAddress ipAdress;

    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime);
    }

    void OnApplicationQuit()
    {
        socket.Close();
        thread.Abort();
        isConnected = false;
        socket = null;
        ipAdress = null;
    }
    private bool Connect()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 4242);

        try
        {
            socket.Connect(ipEndPoint);

            isConnected = true;

            thread = new Thread(new ThreadStart(Data_IN));
            thread.Start();
            return true;
        }
        catch (SocketException ex)
        {
            Debug.Log("Error during connecting to server:\n" + ex + "\n\n");
        }
        return false;
    }
    void Start()
    {
        errorHandler = new ErrorPopup();
        ipAdress = IPAddress.Parse("5.9.251.202");
        int retrys = 4;
        while (!isConnected && retrys >= 0)
        {
            Connect();
            retrys--;
        }
        errorHandler.add(LanguageManager.Instance.GetTextValue("error_Networking_Server_unavaliable_Topic"), LanguageManager.Instance.GetTextValue("error_Networking_Server_unavaliable"));

    }

    private void Data_IN()
    {
        byte[] buffer;
        int readBytes;
        while (true)
        {
            try
            {
                buffer = new byte[socket.SendBufferSize];
                readBytes = socket.Receive(buffer);

                if (readBytes > 0)
                {
                    Packet p = new Packet(buffer);
                    DataManager(p);
                }
            }
            catch (SocketException)
            {
                ConnectionToServerLost();
            }
        }
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
    void DoMyWindow(int windowID)
    {
        if (errorHandler.liste.Count > 0)
        {
            Error err = (Error)errorHandler.liste[0];
            GUI.TextField(new Rect(5, 25, 790, 250), err.getText());
        }
        else
        {
            GUI.TextArea(new Rect(5, 25, 790, 250), "Hier stimmt was nicht, kann Fehlermeldung nicht lesen!");
        }
        if (GUI.Button(new Rect(350, 275, 100, 20), LanguageManager.Instance.GetTextValue("errorwindow_button_okay")))
        {
            errorHandler.liste.RemoveAt(0);
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
    private void DataManager(Packet p)
    {
        // Debug.Log("test");
        switch (p.packetType)
        {
            case PacketType.Registration:
                ID = p.data[0];
                Packet packet = new Packet(PacketType.Chat, ID);
                packet.data.Add(login);
                packet.data.Add("Enter to Chat");
                socket.Send(packet.ToBytes());
                break;


            case PacketType.Chat:
                Debug.Log(p.data[0]);
                break;
            case PacketType.CloseConnection:
                Debug.Log(p.data[1] + "||" + p.data[0]);
                break;
        }
    }

    private void ConnectionToServerLost()
    {
        Debug.Log("Server disconnected");
        socket.Close();
        thread.Abort();
    }
    public static MessageData FromByteArray(byte[] input)
    {
        MemoryStream stream = new MemoryStream(input);

        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        MessageData data = new MessageData();
        data.type = (int)formatter.Deserialize(stream);
        data.stringData = (string)formatter.Deserialize(stream);

        if (data.stringData == "")
        {
            data.type = 999;
            data.stringData = "No Command Included";
        }
        return data;
    }
}