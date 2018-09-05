using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobLevel {

    private long id;
    private string jobName;
    private int level;
    private int xpToLevel;

    // all of the remaining values are their new values when the level changes. this is total, not cumulative
    private int baseStrength;
    private int baseDexterity;
    private int baseSpeed;
    private int baseEndurance;
    private int baseSpirit;
    private int baseIntelligence;
    private int baseWillpower;
    private int baseCharisma;
    private int baseHp;
    private int baseSp;

}
