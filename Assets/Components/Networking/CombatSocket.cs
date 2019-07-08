using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class CombatSocket 
{
    public CombatData combatData;
    public CombatController combatController;
    WebSocket webSocket;

    public void StartConnection(CombatController combatController, GameData gameData)
    {
        this.combatController = combatController;
        webSocket = new WebSocket(RestUtil.COMBAT_WEBSOCKET);
        webSocket.OnOpen += OnOpenHandler;
        webSocket.OnMessage += OnMessageHandler;
        webSocket.OnClose += OnCloseHandler;

        webSocket.ConnectAsync();
        SendTestCombatMessage(gameData);
    }

    public void SendMessage()
    {
        // todo: add messages that need sent for combat actions
        
    }

    public void SendTestCombatMessage(GameData gameData)
    {
        string combatDataMsg = JsonUtility.ToJson(GetTestMessage(gameData));
        Debug.Log("Combat msg sent: " + combatDataMsg);
        webSocket.Send(combatDataMsg);
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
        combatData = JsonUtility.FromJson<CombatData>(e.Data);
        combatController.combatData = combatData;

        Debug.Log("Websocket server got combat data from: " + combatData.playerData.playerId);
    }

    private void OnCloseHandler(object sender, CloseEventArgs e)
    {
        Debug.Log("Websocket closed with reason: " + e.Reason);
    }

    public CombatData GetTestMessage(GameData gameData)
    {
        combatData = new CombatData();
        combatData.gameId = gameData.gameId;
        GameData playerData = new GameData();
        playerData.gameId = gameData.gameId;
        playerData.playerId = gameData.playerId;
        combatData.playerData = playerData;
        combatData.combatState = "combat";
        return combatData;
    }
}
