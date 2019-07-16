using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero {

    public int heroId;
    public int gameId;
    public string playerName;

    public string heroName;         // name of hero
    public int level;               // hero's level - higher level is stronger
    public string className;        // class name will sync back to the job class to grab data needed
    public int xp;                  // experience - at set intervals, level up and gain base stats, unlock skills available
    public int ap;                  // ability points - how to purchase skill for a hero

    // Hero base attributes
    public int baseStrength;        // allows physical skills and attacks to hit harder, and block ability for shields
    public int baseDexterity;       // determines how often hits can miss, crit, and gives a bonus to physical defense
    public int baseSpeed;           // gives a bonus to attack speed, skill speed, and dodge and parry chance
    public int baseEndurance;       // determines bonus hit points and bonus regen % to hit points
    public int baseSpirit;          // determines bonus skill points and bonus regen % to skill points
    public int baseIntelligence;    // determines bonus to magic skills and cause magic attacks to hit harder
    public int baseWillpower;       // gives a bonus to magic deflection, absorption, and resistance
    public int baseCharisma;        // gives a bonus to leadership skills, buying/selling prices, and quest rewards
    public int baseHp;              // hit points before any gear or modifiers
    public int baseSp;              // skill points before any gear or modifiers

    public int bonusStrength;
    public int bonusDexterity;
    public int bonusSpeed;
    public int bonusEndurance;
    public int bonusSpirit;
    public int bonusIntelligence;
    public int bonusWillpower;
    public int bonusCharisma;
    public int bonusHp;
    public int bonusSp;
    public int currentHp;
    public int currentSp;

/*  ITEMS AND SKILLS NOT IMPLEMENTED YET
    // player gear
    public List<Item> equipped;
    public Weapon weapon;
    public Shield shield;
    public Armor boots;
    public Armor chest;
    public Armor gloves;
    public Armor legs;
    public Armor head;
    public Relic ring;
    public Relic neck;
    public Relic ring2;

    // player skills
    public List<Skill> availableSkills;
    public PassiveSkill passive1;
    public PassiveSkill passive2;
    public ActiveSkill autocastSkill;
    public ActiveSkill active1;
    public ActiveSkill active2;
    public ActiveSkill active3;
    public ActiveSkill active4;
    public List<Skill> equippedSkills; */


    // calculated attributes 

    public int totalStrength()
    {
        return baseStrength + bonusStrength;
    }

    public int totalDexterity()
    {
        return baseDexterity + bonusDexterity;
    }

    public int totalSpeed()
    {
        return baseSpeed + bonusSpeed;
    }

    public int totalEndurance()
    {
        return baseEndurance + bonusEndurance;
    }

    public int totalSpirit()
    {
        return baseSpirit + bonusSpirit;
    }

    public int totalIntelligence()
    {
        return baseIntelligence + bonusIntelligence;
    }

    public int totalWillpower()
    {
        return baseWillpower + bonusWillpower;
    }

    public int totalCharisma()
    {
        return baseCharisma + bonusCharisma;
    }

    public int totalHp()
    {
        return baseHp + bonusHp;
    }

    public int totalSp()
    {
        return baseSp + bonusSp;
    }

    public int armorRating()
    {
        return 0;
    }          

    public int resistRating()
    {
        return 0;
    }

    public int maxHp()
    {
        return totalHp();
    }  
    
    public int maxSp()
    {
        return totalSp();
    }

    public float attackSpeed()
    {
        return 1 + ((totalSpeed() / 2) / 100); 
    }

    public int minAutoDamage()
    {
        return totalStrength() * 1;
    }

    public int maxAutoDamage()
    {
        return totalStrength() * 2;
    }

    public int defense()
    {
        return armorRating() + totalDexterity();
    }

    public float blockChance()
    {
        return totalDexterity() * .005f;
    }

    public int block()
    {
        return totalStrength();
    }

    public int resist()
    {
        return resistRating() + totalWillpower();
    }

    public float dodgeChance()
    {
        return 0.05f + totalSpeed() * .005f;
    }

    public float parryChance()
    {
        return 0.05f + totalSpeed() * 0.005f;
    }

    public float castingSpeed()
    {
        return 1 + totalSpeed() * 0.01f;
    }

    public float hitChance()
    {
        return 0.8f + totalDexterity() * 0.005f;
    }

    public float critChance()
    {
        return 0.05f + totalDexterity() * 0.005f;
    }

    public float magicDeflectChance()
    {
        return 0.05f + totalWillpower() * 0.005f;
    }

    public float magicAbsorbChance()
    {
        return 0.05f + totalWillpower() * 0.005f;
    }

    public float leadershipBonus()
    {
        return 1 + totalCharisma() * 0.01f;
    }

    public float regenHpSpeed()
    {
        return 0.01f + totalEndurance() * 0.001f;
    }

    public float regenSpSpeed()
    {
        return 0.01f + totalSpirit() * 0.001f;
    }

    public float priceHagglePercent()
    {
        return totalCharisma() * 0.01f;
    }

    public float questGainPercent()
    {
        return totalCharisma() * 0.01f;
    }

    /*

    public void UpdateHero()
    {
        // go through all the functionality to update a hero.
        SetEquipped();
        CalculateBonuses();
        GenerateAttributes();
    }

    public void SetEquipped()
    {
        equipped.Clear();
        equipped.Add(weapon);
        equipped.Add(shield);
        equipped.Add(boots);
        equipped.Add(chest);
        equipped.Add(gloves);
        equipped.Add(legs);
        equipped.Add(head);
        equipped.Add(ring);
        equipped.Add(neck);
        equipped.Add(ring2);

        equippedSkills.Add(passive1);
        equippedSkills.Add(passive2);
        equippedSkills.Add(autocastSkill);
        equippedSkills.Add(active1);
        equippedSkills.Add(active2);
        equippedSkills.Add(active3);
        equippedSkills.Add(active4);
    }

    public void CalculateBonuses()
    {
        // TODO: loop through each item, and add their attributes to bonuses
        // TODO: go through all passive skills, and add their bonuses
        // TODO: go through all active buffs/debuffs, and add their bonuses/penalties

    } */

    public Hero(string heroName, string playerName, string jobName)
    {
        this.heroName = heroName;
        this.playerName = playerName;
        this.className = jobName;
    }

}
