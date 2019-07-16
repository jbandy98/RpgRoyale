using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataService
{
    public static void updateGameData(GameData gameData)
    {
        string url = RestUtil.GAME_DATA_URI + "update";
        Debug.Log("Sending new gamedata updates to: " + url);
        string jsonData = JsonUtility.ToJson(gameData);
        Debug.Log("GameData json data: " + jsonData);
        string jsonResponse = RestUtil.Instance.Post(url, jsonData).text;
        Debug.Log("Json Response after sending game data: " + jsonResponse);
    }

    public static GameData getGameDataUpdates(int gameId, string playerId)
    {
        string url = RestUtil.GAME_DATA_URI + gameId + "/" + playerId;
        string jsonResponse = RestUtil.Instance.Get(url);
        Debug.Log("Json Response: " + jsonResponse);
        GameData gameData = JsonUtility.FromJson<GameData>(jsonResponse);
        return gameData;
    }
}
