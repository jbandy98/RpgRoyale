using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    public string playerId;            // username of player
    public int gameId;              // the id of the game the player is in
    public GameState gameState;     // the current state of this game to re-manage login if needed
    public List<Hero> party;        // the player's party of 4 heroes
    public Inventory inventory;     // The player's item inventory for this game session (clears each game)
    public int gold;                // the player's gold this game session (clears each game)
    public Vector3 location;        // player's location on the map
    public string sessionId;        // session id for some security
}
