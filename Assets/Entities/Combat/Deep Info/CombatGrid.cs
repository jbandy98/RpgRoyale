using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombatGrid 
{
    public int width = 10;
    public int height = 5;
    public GridLocation[][] location;
    public List<CombatUnit> units;
}
