using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public int enemyId;
    public int gameId;
    public string enemyName;

    public int level;
    public int xpValue;
    public int gpValue;
    public int apValue;

    public int strength;
    public int dexterity;
    public int speed;
    public int endurance;
    public int spirit;
    public int intelligence;
    public int willpower;
    public int charisma;
    public int hp;
    public int sp;

    public int currentHp;
    public int currentSp;
    public int attackRange;
    public string normalAttackDamageType;
    

    public float attackSpeed()
    {
        return 1 + ((speed / 2) / 100);
    }

    public int minAutoDamage()
    {
        return strength * 1;
    }

    public int maxAutoDamage()
    {
        return strength * 2;
    }

    public int armorRating()
    {
        return 0;
    }

    public int resistRating()
    {
        return 0;
    }

    public int defense()
    {
        return armorRating() + dexterity;
    }

    public float blockChance()
    {
        return dexterity * .005f;
    }

    public int block()
    {
        return strength;
    }

    public int resist()
    {
        return resistRating() + willpower;
    }

    public float dodgeChance()
    {
        return 0.05f + speed * .005f;
    }

    public float parryChance()
    {
        return 0.05f + speed * 0.005f;
    }

    public float castingSpeed()
    {
        return 1 + speed * 0.01f;
    }

    public float hitChance()
    {
        return 0.8f + dexterity * 0.005f;
    }

    public float critChance()
    {
        return 0.05f + dexterity * 0.005f;
    }

    public float magicDeflectChance()
    {
        return 0.05f + willpower * 0.005f;
    }

    public float magicAbsorbChance()
    {
        return 0.05f + willpower * 0.005f;
    }

    public float leadershipBonus()
    {
        return 1 + charisma * 0.01f;
    }

    public float regenHpSpeed()
    {
        return 0.01f + endurance * 0.001f;
    }

    public float regenSpSpeed()
    {
        return 0.01f + spirit * 0.001f;
    }
}
