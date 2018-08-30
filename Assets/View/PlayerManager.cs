using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour {

    public PlayerData data;
    public TextMeshPro playerText;

	// Use this for initialization
	void Start () {
        playerText.text = data.playerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
