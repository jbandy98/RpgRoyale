using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService 
{
    public static void startNewGame(int gameId)
    {
        string url = RestUtil.GAME_SERVER_URI + "startgame/" + gameId;
        string jsonResponse = RestUtil.Instance.Get(url);
    }
}
