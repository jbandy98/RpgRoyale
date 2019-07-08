using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService
{

    public static List<Encounter> createNewGameEncounterData(int gameId)
    {
        string url = RestUtil.ENEMY_SERVICE_URI + "createEncounters/" + gameId;
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("encounters", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Encounters encounters = JsonUtility.FromJson<Encounters>(jsonResponse);
        List<Encounter> encounterList = encounters.encounters;
        return encounterList;
    }

    public static List<Encounter> getUpdatedEncounterData(int gameId)
    {
        string url = RestUtil.ENEMY_SERVICE_URI + gameId;
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("encounters", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Encounters encounters = JsonUtility.FromJson<Encounters>(jsonResponse);
        List<Encounter> encounterList = encounters.encounters;
        return encounterList;
    }

    public static Encounter findEncounterAtPlayerLoc(int gameId, string playerId)
    {
        string url = RestUtil.ENEMY_SERVICE_URI + gameId + "/" + playerId;
        string jsonResponse = RestUtil.Instance.Get(url);
        Encounter encounter = JsonUtility.FromJson<Encounter>(jsonResponse);
        return encounter;
    }
}
