using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneController : MonoBehaviour {

    public static List<Job> jobs;
    public static PlayerData playerData;
    public List<Dropdown> heroClass;
    public List<Text> heroName;
    public string username;

	// Use this for initialization
	void Awake () {
        string url = RestUtil.JOB_SERVICE_URI + "/jobs";
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("jobs", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Jobs jobsList = JsonUtility.FromJson<Jobs>(jsonResponse);
        jobs = jobsList.jobs;

        Debug.Log("Jobs count: " + jobs.Count);
        foreach (Job job in jobs) {
            Debug.Log("Job: " + job.jobName + " Role: " + job.roles + " description: " + job.description + " max level: " + job.maxLevel);
        }

        // get the player data
        username = PlayerPrefs.GetString("user");
        string playerUrl = RestUtil.PLAYER_SERVICE_URI + "player/" + username;
        string playerJsonResponse = RestUtil.Instance.Get(playerUrl);
        Debug.Log("Player data json response: " + playerJsonResponse);
        playerData = JsonUtility.FromJson<PlayerData>(playerJsonResponse);
	}
	
    public void PlayGameClick()
    {
        // save the player's party data
        Hero[] party = new Hero[4];

        for(int i = 0; i < 4; i++)
        {
            party[i] = new Hero(heroName[i].text, username, heroClass[i].options[heroClass[i].value].text);
            party[i].level = 1;
            string url = RestUtil.HERO_SERVICE_URI + "hero/create";
            Debug.Log("Sending create hero request to: " + url);
            string jsonData = JsonUtility.ToJson(party[i]);
            Debug.Log("Hero json data: " + jsonData);
            string jsonResponse = RestUtil.Instance.Post(url, jsonData).text;
            Debug.Log("Json Response after creating new hero: " + jsonResponse);
        }


        // start a waiting for players notification

        // locate a set of other players

        // start the game
        StartCoroutine(StartGameScene());
    }

    IEnumerator StartGameScene()
    {
        WaitForSeconds w;
        w = new WaitForSeconds(1.0f);
        Debug.Log("Starting game scene now!");
        SceneManager.LoadScene("MainGame");
        yield return w;
    }
}
