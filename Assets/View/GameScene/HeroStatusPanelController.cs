using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStatusPanelController : MonoBehaviour {

    public Text heroName;
    public Text level;
    public Text className;
    public Text currentHp;
    public Text maxHp;
    public Text currentSp;
    public Text maxSp;
    public Text currentXp;
    public Text nextLevelXp;
    public Text statusEffects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStats(Hero hero)
    {
        heroName.text = hero.heroName;
        level.text = hero.level.ToString();
        className.text = hero.className;
        currentHp.text = hero.currentHp.ToString();
        maxHp.text = hero.maxHp().ToString();
        currentSp.text = hero.currentSp.ToString();
        maxSp.text = hero.maxSp().ToString();
        currentXp.text = hero.xp.ToString();
        nextLevelXp.text = 1000.ToString();
        statusEffects.text = "";

    }
}
