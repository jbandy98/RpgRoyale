using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int gameId;
    public int enemyCount;
    public List<Encounter> encounters;
    public Dictionary<int, GameObject> encounterObjects;
    public GameObject enemyPrefab;
    public float updateTime = 2.0f;
    public float timeElapsed;

    void Start()
    {
        encounterObjects = new Dictionary<int, GameObject>();

        // TODO: remove hard coded game Id
        gameId = 1;

        timeElapsed = 0;

    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > updateTime)
        {
            updateEnemies();
            timeElapsed = 0;
        }

    }

    void updateEnemies()
    {
        // get updated encounter data to move
        encounters = EnemyService.getUpdatedEncounterData(gameId);
        foreach(Encounter encounter in encounters)
        {
            GameObject encounterObj;
            
            if(encounterObjects.TryGetValue(encounter.encounterId, out encounterObj))
            {
                encounterObj.GetComponent<EnemyController>().encounter = encounter;
            } else
            {
                GameObject enemySpawn = PhotonNetwork.Instantiate(enemyPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
                enemySpawn.GetComponent<EnemyController>().encounter = encounter;
                encounterObjects.Add(encounter.encounterId, enemySpawn);
            }
        }
    }
}
