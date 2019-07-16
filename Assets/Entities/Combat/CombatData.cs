using System.Collections.Generic;

[System.Serializable]
public class CombatData
{
    public string playerId;
    public int gameId;
    public int encounterId;
    public string gameState;
    public List<CombatDataUnit> dataUnits;
}
