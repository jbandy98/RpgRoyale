using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[JsonConverter(typeof(CombatUnitConverter))]
public class CombatUnit 
{
    public int unitId;
    public int referenceId;
    public int ObjType { get; set; }
    public bool isHero;
    public int locX;
    public int locY;
    public int targetX;
    public int targetY;    
    public string aiStrategy;
    public int currentTarget;
    public bool isIncapacitated;
    public bool isDead;
    public bool isCasting;
    public bool isMoving;
    public bool isAttacking;
    public bool moveComplete;
    public bool castingComplete;
    public bool manualTarget;
    public List<ThreatLevel> threatLevels;

}
