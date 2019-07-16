using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CombatService 
{

    public static CombatData startNewCombat(string playerId, int gameId, int encounterId)
    {
        string url = RestUtil.COMBAT_SERVER_URI + "startCombat/" + playerId + "/" + gameId + "/" + encounterId;
        string jsonResponse = RestUtil.Instance.Get(url);
        Debug.Log("Combat data response: " + jsonResponse);
        CombatData combatData = JsonConvert.DeserializeObject<CombatData>(jsonResponse);
        return combatData;
    }

    public static LootData getCombatResults(string playerId, int gameId, int encounterId)
    {
        string url = RestUtil.COMBAT_SERVER_URI + "endCombat/" + playerId + "/" + gameId + "/" + encounterId;
        string jsonResponse = RestUtil.Instance.Get(url);
        Debug.Log("Loot data response: " + jsonResponse);
        LootData lootData = JsonConvert.DeserializeObject<LootData>(jsonResponse);
        return lootData;
    }
}
