using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour {

    public static List<Job> jobs;
    public static PlayerData playerData;

	// Use this for initialization
	void Awake () {
        string url = RestUtil.JOB_SERVICE_URI + "/jobs";
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("job", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Jobs jobsList = JsonUtility.FromJson<Jobs>(jsonResponse);
        jobs = jobsList.jobs;

        Debug.Log("Jobs count: " + jobs.Count);
        foreach (Job job in jobs) {
            Debug.Log("Job: " + job.jobName + " Role: " + job.roles + " description: " + job.description + " max level: " + job.maxLevel);
        }

        // get the player data
        string username = PlayerPrefs.GetString("user");
        string playerUrl = RestUtil.PLAYER_SERVICE_URI + "/player/" + username;
        string playerJsonResponse = RestUtil.Instance.Get(playerUrl);
        Debug.Log("Player data json response: " + playerJsonResponse);
        playerData = JsonUtility.FromJson<PlayerData>(playerJsonResponse);
	}
	
    public void PlayGameClick()
    {
        // save the player's party data

        // start a waiting for players notification

        // locate a set of other players

        // start the game
        SceneManager.LoadScene("MainGame");
    }
}
