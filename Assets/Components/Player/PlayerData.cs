using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public string playerName;
    public Vector3Int location;
    public string zoneName;
    public string sessionId;
    public int wins;                // amount of times player has won
    public int losses;              // amount of times player has lost
    public double averagePlace;     // in normal games, what the average place is player finishes

    public List<Hero> heroes;       // list of the heroes the player has in game
    public GameState gameState;     // keeps track of current state (i.e. battle, menu, etc.)
    public int gameId;              // keeps track of the current game they are in, if any

    public int xp;
    public int level;
    public int elo;
    public int diamonds;


    // TODO: Add trait system
    // TODO: Add 'inventory' showing available traits, skills, passives, and classes to use during game
    List<Trait> availableTraits;
    List<Skill> availableSkills;
    List<PassiveSkill> availablePassiveSkills;
    List<Job> unlockedClasses;

    // game time variables
    public Inventory inventory;         // item inventory
    public int gold;                    // party gold


}
