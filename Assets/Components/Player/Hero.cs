using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero {

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

    public int totalStrength;
    public int totalDexterity;
    public int totalSpeed;
    public int totalEndurance;
    public int totalSpirit;
    public int totalIntelligence;
    public int totalWillpower;
    public int totalCharisma;
    public int totalHp;
    public int totalSp;
    public int armorRating;             // total amount of armor points across all items
    public int resistRating;            // total amount of resist point across all items

    public int currentHp;
    public int maxHp;
    public int currentSp;
    public int maxSp;

    // combat attributes - generated once at the beginning of each combat, based on gear and other bonuses
    public float attackSpeed;
    public int minAutoDamage;
    public int maxAutoDamage;
    public int defense;
    public float blockChance;
    public float block;
    public int resist;
    public float dodgeChance;
    public float parryChance;
    public float castingSpeed;
    public float hitChance;
    public float critChance;
    public float magicDeflectChance;
    public float magicAbsorbChance;
    public float leadershipBonus;
    public float regenHpSpeed;
    public float regenSpSpeed;

    // when in towns, charisma comes into play
    public float priceHagglePercent;
    public float questGainPercent;

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
    public List<Skill> equippedSkills;

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

    }

    public void GenerateAttributes()
    {
        // this function will generate all the bonuses above based on gear, passives, buffs, etc.
        attackSpeed = 1 + ((totalSpeed / 2) / 100) * weapon.weaponSpeedModifier;
        minAutoDamage = weapon.minDamage + (totalStrength);
        maxAutoDamage = weapon.maxDamage + (totalStrength);
        defense = armorRating + (totalDexterity);
        blockChance = shield.blockChance + (baseDexterity * .005f);
        block = shield.blockRating + (totalStrength);
        resist = resistRating + (totalWillpower);
        dodgeChance = 0.05f + (totalSpeed * .005f);
        parryChance = 0.05f + (totalSpeed * .005f);
        castingSpeed = 1 + (totalSpeed * .01f);
        hitChance = 0.8f + (totalDexterity * .005f);
        critChance = 0.05f + (totalDexterity * .005f);
        magicDeflectChance = 0.05f + (totalWillpower * .005f);
        magicAbsorbChance = 0.05f + (totalWillpower * .005f);
        leadershipBonus = 1 + (totalCharisma * 0.01f);
        priceHagglePercent = (totalCharisma * 0.01f);
        questGainPercent = (totalCharisma * 0.01f);
        regenHpSpeed = 0.01f + (totalEndurance * 0.001f);
        regenSpSpeed = 0.01f + (totalSpirit * 0.001f);
        maxHp = totalHp + (totalEndurance * 2);
        maxSp = totalSp + (totalSpirit * 2);
    }
}
