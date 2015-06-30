using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using PacketConnector;

public class Client : MonoBehaviour
{
    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
    }

    public string ID = "40";
    public string login = "User:zee";

    private bool isConnected = false;
    private Socket socket;
    private Thread thread;
    private IPAddress ipAdress;

    void OnApplicationQuit()
    {
        socket.Close();
        thread.Abort();
        isConnected = false;
        socket = null;
        ipAdress = null;
    }

    void Start()
    {
        ipAdress = IPAddress.Parse("127.0.0.1");
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 4242);

        try
        {
            socket.Connect(ipEndPoint);

            isConnected = true;

            thread = new Thread(new ThreadStart(Data_IN));
            thread.Start();
        }
        catch (SocketException ex)
        {
            Debug.Log("Error during connecting to server:\n" + ex + "\n\n");
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