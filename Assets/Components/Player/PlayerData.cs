using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    // player variables
    public string name;
    public string session;
    public int xp;
    public int level;
    public int diamonds;

    // player statistics
    public int wins;                // amount of times player has won
    public int losses;              // amount of times player has lost
    public double avgplace;     // in normal games, what the average place is player finishes
    public int elo;                 // a number showing the strength of the player vs others


    // TODO: Add trait system
    // TODO: Add 'inventory' showing available traits, skills, passives, and classes to use during game
    //List<Trait> availableTraits;
    //List<Skill> availableSkills;
    //List<PassiveSkill> availablePassiveSkills;
    //List<Job> unlockedClasses;

}
