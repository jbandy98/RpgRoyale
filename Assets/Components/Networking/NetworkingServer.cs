using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class NetworkingServer : MonoBehaviour
{

    private List<Client> clients;
    private List<Client> disconnectList;

    public int port = 6321;
    private TcpListener server;
    private bool serverStarted;

    void Awake()
    {
        clients = new List<Client>();
        disconnectList = new List<Client>();

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port " + port);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    void Update()
    {
        if (!serverStarted)
            return;

        foreach (Client client in clients)
        {
            // is the client still connected
            if (!IsConnected(client.tcpClient))
            {
                client.tcpClient.Close();
                disconnectList.Add(client);
                continue;
            }
            // check for messages from the client
            else
            {
                NetworkStream networkStream = client.tcpClient.GetStream();
                if (networkStream.DataAvailable)
                {
                    StreamReader reader = new StreamReader(networkStream, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(client, data);
                    }
                }
            }
        }
    }

    private bool IsConnected(TcpClient client)
    {
        try
        {
            if (client != null && client.Client != null && client.Client.Connected)
            {
                if (client.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(client.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private void OnIncomingData(Client client, string data)
    {
        Debug.Log(client.clientName + " has sent the following message: " + data);
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        clients.Add(new Client(listener.EndAcceptTcpClient(ar)));
        StartListening();

        // Send a message to everyone
        Broadcast(clients[clients.Count - 1].clientName + " has connected", clients);
    }

    private void Broadcast(string data, List<Client> clients)
    {
        foreach(Client client in clients)
        {
            try
            {
                StreamWriter writer = new StreamWriter(client.tcpClient.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            } catch (Exception e)
            {
                Debug.Log("Write error: " + e.Message + " to client " + client.clientName);
            }
        }
    }

    public class Client
    {
        public TcpClient tcpClient;
        public string clientName;

        public Client(TcpClient clientSocket)
        {
            clientName = "Guest";
            tcpClient = clientSocket;
        }
    }
}

