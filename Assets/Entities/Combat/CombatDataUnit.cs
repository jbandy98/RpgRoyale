using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatDataUnit 
{
    public int unitId;
    public string unitName;
    public int level;
    public bool isHero;
    public int locX;
    public int locY;
    public int targetX;
    public int targetY;
    public int hp;
    public int maxHp;
    public int sp;
    public int maxSp;
    public string aiStrategy;
    public int currentTarget;
    public bool isIncapacitated;
    public bool isDead;
    public bool isCasting;
    public bool isMoving;
    public bool moveComplete;
    public bool isAttacking;
    public bool castingComplete;
}
