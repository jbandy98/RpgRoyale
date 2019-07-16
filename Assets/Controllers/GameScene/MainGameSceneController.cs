using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSceneController : MonoBehaviour {

    // static variables to capture specific game data
    public static PlayerData playerData;
    public static List<Hero> party;
    public GameData gameData;
    public static string username;
    public HeroStatusPanelController[] statusPanels;
    public int gameId;
    public string gameState;
    public GameObject combatWindow;
    public float gameUpdateTime = 0.5f;
    private float elapsedTime;


	// Use this for initialization
	void Start () {
        //TODO: Remove hard coded game id
        gameId = 1;
        // get the player data
        username = PlayerPrefs.GetString("user");
        string playerUrl = RestUtil.PLAYER_SERVICE_URI + username;
        string playerJsonResponse = RestUtil.Instance.Get(playerUrl);
        Debug.Log("Player data json response: " + playerJsonResponse);
        playerData = JsonUtility.FromJson<PlayerData>(playerJsonResponse);
        
        // get the party data
        string url = RestUtil.HERO_SERVICE_URI + "user/" + username;
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("party", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Party partyObj = JsonUtility.FromJson<Party>(jsonResponse);
        party = partyObj.party;
        if (party == null)
        {
            Debug.LogError("No hero objects found for player.");
        }
        gameState = "world";
        // this loop updates the hero status panels
        for (int i = 0; i < 4; i++)
        {
            statusPanels[i].UpdateStats(party[i]);
        }
        combatWindow.SetActive(false);
        GameService.startNewGame(gameId);
        elapsedTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > gameUpdateTime)
        {
            gameData = GameDataService.getGameDataUpdates(gameId, username);
            elapsedTime = 0f;
        }
	}

    public void ExitGameClick()
    {
        SceneManager.LoadScene("Title");
    }
}
