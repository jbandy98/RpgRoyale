using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;

public class WebSocketSharpDemo : MonoBehaviour
{
    public Text hostField;
    public Text messageField;

    WebSocket webSocket;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartConnection()
    {
        webSocket = new WebSocket(hostField.text);
        webSocket.OnOpen += OnOpenHandler;
        webSocket.OnMessage += OnMessageHandler;
        webSocket.OnClose += OnCloseHandler;

        webSocket.ConnectAsync();
    }

    public void SendMessage()
    {
        webSocket.Send(messageField.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnOpenHandler(object sender, System.EventArgs e)
    {
        Debug.Log("Websocket connected!");
    }

    private void OnSendComplete(bool success)
    {
        Debug.Log("Message sent successfully? " + success);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e)
    {
        Debug.Log("Websocket server said: " + e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e)
    {
        Debug.Log("Websocket closed with reason: " + e.Reason);
    }
}
