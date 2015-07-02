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
    
    public string ID = "0";
    public string login = "Aileen";

    private bool isConnected = false;
    private Socket socket;
    private Thread thread;
    private IPAddress ipAdress;


    public void CloseConnection()
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
    public void Start()
    {
        ipAdress = IPAddress.Parse("5.9.251.202");
        int retrys = 4;
        while (!isConnected && retrys >= 0)
        {
            Connect();
            retrys--;
        }
        if (!isConnected)
        {
            Main.errorHandler.add(LanguageManager.Instance.GetTextValue("error_Networking_Server_unavaliable_Topic"), LanguageManager.Instance.GetTextValue("error_Networking_Server_unavaliable"));
        }

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
    private void DataManager(Packet p)
    {
        switch (p.packetType)
        {
            case PacketType.getNews:
                Debug.Log("Got News!");
                string news = "";
                foreach (string values in p.data)
                {
                    news += values;
                }
                MainMenu.news = news;
                break;
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
    public void send(Packet p)
    {
        socket.Send(p.ToBytes());
    }
    private void ConnectionToServerLost()
    {
        Main.errorHandler.add("Connection Lost", "Your Connection to the Server has beed Terminated.\nTry Again Later");
        Debug.Log("Server disconnected");
        socket.Close();
        thread.Abort();
    }
}