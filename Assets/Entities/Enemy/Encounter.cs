using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Encounter
{
    public int encounterId;
    public int gameId;
    public int xLoc;
    public int yLoc;
    public List<Enemy> enemies;
}
