using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class NetworkingClient : MonoBehaviour
{
    public string host = "localhost/combat/connect";
    public int port = 7112;
    private bool socketReady = false;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamReader reader;
    private StreamWriter writer;
    public Text hostField;
    public Text portField;

    void Start()
    {
        hostField.text = host;
        portField.text = port.ToString();

    }

    public void StartConnection()
    {
        host = hostField.text;
        port = Int32.Parse(portField.text);
        OnConnectedToServer();
    }

    public void OnConnectedToServer()
    {
        // if already connected, ignore this function
        if (socketReady)
            return;

        // create the socket
        try
        {

            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            Debug.Log("Client created?");

        } catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    void Update()
    {
        if(socketReady)
        {
            if(stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnIncomingData(string data)
    {
        Debug.Log("Server: " + data);
    }


}
