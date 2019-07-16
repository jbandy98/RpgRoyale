using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
{

    GroundType groundType;
    int x;
    int y;
    World world;
    Action<Tile> cbTileTypeChanged;

    public Tile(int x, int y, World world)
    {
        this.x = x;
        this.y = y;
        this.world = world;
    }

    /// <summary>
    /// Register a function to be called back when our tile type changes.
    /// </summary>
    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    /// <summary>
    /// Unregister a callback.
    /// </summary>
    public void UnegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }

    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }

    public World World
    {
        get { return world; }
        set { world = value; }
    }

    public GroundType GroundType
    {
        get { return groundType; }
        set { groundType = value; }
    }
}

