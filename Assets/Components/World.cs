using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World  {

    Tile[,] tiles;
    int width;
    int height;
    string name;

    public World (int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(x, y, this);
                tiles[x, y].GroundType = GroundType.WATER;
            }
        }

        GenerateWorld();
    }

    void GenerateWorld()
    {
        // this is the primary function used to generate a game world

        // algorithm idea:
        // 1. 1st pass - create a 'continent'
        // 2. 2nd pass - create rivers and lakes
        // 3. 3rd pass - create deserts, swamps, and forests
        // 4. 4th pass - create mountain ranges and hill areas
        // 5. 5th pass - create dungeons and towns (future addition)

        // 1. create a continent - use grass tiles to make a continent

        // do a pass to make the inner square continent
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x > 7 && x < width - 7 && y > 7 && y < height - 7)
                {
                    tiles[x, y].GroundType = GroundType.GRASSLAND;
                }
            }
        }

        // 2nd pass for continent - make a random range of 4-8 tiles in from each edge, and fill all tiles farther in that that as grass

        for (int y = 0; y < height; y++)
        {
            int randLeft = Random.Range(0, 4);
            int randRight = Random.Range(0, 4);
            for (int x = 0; x < width; x++)
            {
                // left side
                if (x > 4 + randLeft && x < 8 && y > 8 && y < height - 8)
                {
                    tiles[x, y].GroundType = GroundType.GRASSLAND;
                }

                // right side
                if (x < width - 8 + randRight && x > width - 8 && y > 8 && y < height - 8)
                {
                    tiles[x, y].GroundType = GroundType.GRASSLAND;
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            int randTop = Random.Range(0, 4);
            int randBot = Random.Range(0, 4);
            for (int y = 0; y < height; y++)
            {
                // top side
                if (y > 4 + randTop && y < 8 && x > 8 && x < width - 8)
                {
                    tiles[x, y].GroundType = GroundType.GRASSLAND;
                }

                // bot side
                if (y < height - 8 + randBot && y > height - 8 && x > 8 && x < width - 8)
                {
                    tiles[x, y].GroundType = GroundType.GRASSLAND;
                }
            }
        }

        // 2. create lakes and rivers

    }

    public Tile GetTileAt(int x, int y)
    {
        return tiles[x, y];
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    public int Height
    {
        get { return height; }
        set { height = value; }
    }
}
