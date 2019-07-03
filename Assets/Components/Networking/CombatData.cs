using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatData
{
    public int gameId;
    public int encounterId;
    public GameData playerData;
    public List<HeroUnit> heroUnits;
    public List<EnemyUnit> enemyUnits;
    public List<CombatUnit> allUnits;
    public CombatGrid combatGrid;
    public int gpEarned;
    public int xpEarned;
    public int apEarned;
    public string combatState;
}
