using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnit 
{
    public bool isHero;
    public GridLocation location;
    public GridLocation movingTo;
    public List<ThreatLevel> threatLevels;
    public AIStrategy aiStrategy;
    public CombatUnit currentTarget;
    public bool isIncapacitated;
    public bool isDead;
    public bool isCasting;
    public bool isMoving;
    public bool isAttacking;
    public bool moveComplete;
    public bool castingComplete;
    public bool manualTarget;
    public CombatGrid combatGrid;
}
