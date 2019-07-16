using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This message object is used to store data passed across the websocket connection. Summary
 * items being used to keep the data transmitted as small as possible. Goal is to send this
 * object twice per second for updates to the player. (about 6-7 kb per second down)
 * All list items except heroes will use an algorithm to only pull objects in range (10 unit radius)
 */
[System.Serializable]
public class GameDataMessage
{
    public string playerId;                     // username of player
    public int gameId;                          // the id of the game the player is in
    public string state;                    // the current state of this game for the player
    public int combatId;               // the id of the encounter player is currently attacking
    public int gp;                            // the player's gold this game session (clears each game)
    public int x;                            // player's location on the map x axis
    public int y;                            // player's location on the y axis
    public List<HeroSummary> heroes;            // list of the players heroes with only hp, sp, and status effect updates
    public List<EncounterSummary> encounters;   // list of nearby encounters with level and image
    public List<PlayerSummary> players;         // list of nearby players with level and image
    public List<EventSummary> events;           // list of nearby events (towns, resources, traps, etc.) with basic image and location data
}
