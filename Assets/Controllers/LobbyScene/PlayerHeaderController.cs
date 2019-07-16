using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeaderController : MonoBehaviour {

    public Text playerName;
    public Text level;
    public Text elo;
    public Text diamonds;
    public Text wins;
    public Text losses;
    public Text avgPlace;

    PlayerData playerData;

	// Use this for initialization
	void Start () {
        playerData = LobbySceneController.playerData;
        UpdatePlayerInfo();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlayerInfo()
    {
        playerName.text = playerData.name;
        level.text = playerData.level.ToString();
        elo.text = playerData.elo.ToString();
        diamonds.text = playerData.diamonds.ToString();
        wins.text = playerData.wins.ToString();
        losses.text = playerData.losses.ToString();
        avgPlace.text = playerData.avgplace.ToString();
    }
}
