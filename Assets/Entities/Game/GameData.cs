using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public string playerId;         // username of player
    public int gameId;              // the id of the game the player is in
    public string gameState;        // the current state of this game to re-manage login if needed
    public int combatEncounterId;   // the id of the encounter player is currently attacking
    public int gold;                // the player's gold this game session (clears each game)
    public int locX;                // player's location on the map x axis
    public int locY;                // player's location on the y axis
}
