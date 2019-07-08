using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatService 
{

    public static CombatData startNewCombat(string playerId, int gameId, int encounterId)
    {
        string url = RestUtil.COMBAT_SERVER_URI + "startCombat/" + playerId + "/" + gameId + "/" + encounterId;
        string jsonResponse = RestUtil.Instance.Get(url);
        CombatData combatData = JsonUtility.FromJson<CombatData>(jsonResponse);
        return combatData;
    }
}
