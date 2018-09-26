using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbySceneController : MonoBehaviour {

    public static List<Job> jobs;
    public static PlayerData playerData;
    public List<Dropdown> heroClass;
    public List<Text> heroName;
    public string username;
    public GameObject setupGamePanel;
    public GameObject gameLobbyPanel;
    public LobbyManager lobbyManager;

    public InputField chatInput;
    public float chatDelay = 2.0f;
    public Text chatText;
    public Text playersJoined;
    PhotonView photonView;
    public float timeElapsed;
    public bool sentText = false;

	// Use this for initialization
	void Awake () {
        photonView = GetComponent<PhotonView>();

        string url = RestUtil.JOB_SERVICE_URI + "/jobs";
        string jsonResponse = RestUtil.Instance.Get(url);
        jsonResponse = RestUtil.fixJson("jobs", jsonResponse);
        Debug.Log("Json Response: " + jsonResponse);
        Jobs jobsList = JsonUtility.FromJson<Jobs>(jsonResponse);
        jobs = jobsList.jobs;
        lobbyManager = GetComponent<LobbyManager>();

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
	
    public void FindGameClick()
    {

        // disable the setup party panel, and enable the game lobby panel
        setupGamePanel.SetActive(false);
        gameLobbyPanel.SetActive(true);

        // need to see if the player already has heroes created. if so, destroy them
        string cleanupUrl = RestUtil.HERO_SERVICE_URI + "hero/cleanup/" + username;
        Debug.Log("Sending call to : " + cleanupUrl);
        string cleanupResponse = RestUtil.Instance.Get(cleanupUrl);
        Debug.Log("Json Response: " + cleanupResponse);

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

        // enable the game lobby and update player list
        lobbyManager.ConnectToNetwork();
        PhotonNetwork.player.NickName = username;
    }

    public void SendChatTextClick()
    {
        if (username == "")
        {
            username = "na";
        }
        // if a player sends a message, start a new line, prefix it with their username, and then the text
        Debug.Log("in send chat text click");
        string message = "\n" + username + ": " + chatInput.text;
        chatInput.text = "";
        chatInput.Select();
        chatInput.ActivateInputField();
        photonView.RPC("UpdateChatBox", PhotonTargets.All, message);       
    }

    [PunRPC]
    public void UpdateChatBox(string message)
    {
        chatText.text = chatText.text + message;
    }

    public void ReturnToSetupClick()
    {
        gameLobbyPanel.SetActive(false);
        setupGamePanel.SetActive(true);

        // remove the player from the game lobby

    }

    public void UpdatePlayerList()
    {
        photonView.RPC("UpdatePlayers", PhotonTargets.All);
        photonView.RPC("UpdateChatBox", PhotonTargets.All, "\n" + username + " joined the game.");
    }

    [PunRPC]
    public void UpdatePlayers()
    {
        playersJoined.text = "Players joined: " + lobbyManager.GetPlayersList(lobbyManager.room);
    }

    public void PlayGame()
    {
        // trigger this function after a delay of ten seconds after all players have clicked ready

        // get the game map

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

    public void Update()
    {
        if (sentText)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > chatDelay)
            {
                sentText = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log("chat focused: " + chatInput.isFocused + " sentText flag: " + sentText + " sendText: " + chatInput.text);
            if (!sentText && chatInput.text != "")
            {
                Debug.Log("Sending text to chat box: " + chatInput.text);

                SendChatTextClick();
                sentText = true;
            }
        }
    }
}
