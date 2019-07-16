using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

public class CombatSocket 
{
    public CombatData combatData;
    public CombatController combatController;
    public string playerId;
    WebSocket webSocket;

    public void StartConnection(CombatController combatController, string playerId)
    {
        this.combatController = combatController;
        webSocket = new WebSocket(RestUtil.COMBAT_WEBSOCKET);
        webSocket.OnOpen += OnOpenHandler;
        webSocket.OnMessage += OnMessageHandler;
        webSocket.OnClose += OnCloseHandler;
        this.playerId = playerId;
        webSocket.ConnectAsync();
        
    }

    public void SendMessage()
    {
        // todo: add messages that need sent for combat actions
        
    }

    private void SendTestCombatMessage(string playerId)
    {
        string combatDataMsg = JsonUtility.ToJson(GetTestMessage(playerId));
        Debug.Log("Combat msg sent: " + combatDataMsg);
        webSocket.Send(combatDataMsg);
    }

    private void OnOpenHandler(object sender, System.EventArgs e)
    {
        Debug.Log("Websocket connected!");
        SendTestCombatMessage(playerId);
    }

    private void OnSendComplete(bool success)
    {
        Debug.Log("Message sent successfully? " + success);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e)
    {
        combatData = JsonConvert.DeserializeObject<CombatData>(e.Data);
        combatController.combatData = combatData;

        Debug.Log("Got combat data from server: " + e.Data);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e)
    {
        Debug.Log("Websocket closed with reason: " + e.Reason);
    }

    public CombatData GetTestMessage(string playerId)
    {
        combatData = new CombatData();
        combatData.playerId = playerId;
        return combatData;
    }
}
