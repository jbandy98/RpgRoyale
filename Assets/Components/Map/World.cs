using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World  {

    Tile[,] tiles;
    int width;
    int height;
    string name;
    const int LAKE_COUNT = 40;
    const int FOREST_COUNT = 200;
    const int HILL_COUNT = 50;
    const int SWAMP_COUNT = 20;
    const int MOUNTAIN_COUNT = 20;
    const int DESERT_COUNT = 25;

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

        Debug.Log("Generating forests.");
        for (int i = 0; i < FOREST_COUNT; i++)
        {
            int randSize = Random.Range(1, 8);
            int randomX = Random.Range(12 + randSize, width - 12 - randSize);
            int randomY = Random.Range(12 + randSize, height - 12 - randSize);
            
            MakeTerrainSpot(randomX, randomY, randSize, GroundType.FOREST, 0.12f);
        }

        Debug.Log("Generating hills.");
        for (int i = 0; i < HILL_COUNT; i++)
        {
            int randSize = Random.Range(1, 4);
            int randomX = Random.Range(12 + randSize, width - 12 - randSize);
            int randomY = Random.Range(12 + randSize, height - 12 - randSize);
            MakeTerrainSpot(randomX, randomY, randSize, GroundType.HILL, 0.12f);
        }

        Debug.Log("Generating swamps.");
        for (int i = 0; i < SWAMP_COUNT; i++)
        {
            int randSize = Random.Range(1, 6);
            int randomX = Random.Range(12 + randSize, width - 12 - randSize);
            int randomY = Random.Range(12 + randSize, height - 12 - randSize);
            MakeTerrainSpot(randomX, randomY, randSize, GroundType.SWAMP, 0.5f);
        }

        Debug.Log("Generating deserts.");
        for (int i = 0; i < DESERT_COUNT; i++)
        {
            int randSize = Random.Range(1, 7);
            int randomX = Random.Range(12 + randSize, width - 12 - randSize);
            int randomY = Random.Range(12 + randSize, height - 12 - randSize);
            MakeTerrainSpot(randomX, randomY, randSize, GroundType.DESERT, 0.01f);
        }

        Debug.Log("Generating lakes.");
        for (int i = 0; i < LAKE_COUNT; i++)
        {
            int randSize = Random.Range(2, 5);
            int randomX = Random.Range(20 + randSize, width - 20 - randSize);
            int randomY = Random.Range(20 + randSize, height - 20 - randSize);
            MakeTerrainSpot(randomX, randomY, randSize, GroundType.WATER, 0.0f);
        }

    }

    void MakeRiver()
    {

    }

    void MakeTerrainSpot(int centerX, int centerY, int radius, GroundType groundToPlace, float reductionPercent)
    {
        // start in the position sent (the center) and move outwards in a swirling motion. Each time 
        // we move to a new radius, decrease the chance that the tile will become the new groundtype
        // by the reductionPercent. Keep going in circles until the tilesToChange is 0
        int currentRadius = 0;
        float changePercent = 1;
        int tilesToChange = 0;
        for (int n = 1; n <= radius; n++)
        {
            tilesToChange += n * 8;
        }

        // set the starting position to groundType
        tiles[centerX, centerY].GroundType = groundToPlace;

        int currentX = centerX;
        int currentY = centerY;
        int tilesToMove = 0;
        while (tilesToChange > 0)
        {
            // generate the loop
            // move up and to the right
            currentX += 1;
            currentY += 1;

            // calculate new radius and changePercent
            currentRadius++;
            changePercent -= reductionPercent;
            if (changePercent < 0.5)
            {
                changePercent = 0.25f;
            }

            tilesToMove = currentRadius * 2;

            // START CURRENT RADIUS LOOP

            // down movement
            for (int down = 0; down < tilesToMove; down++)
            {              
                if (Random.Range(0.0f,1.0f) <= changePercent)
                {
                    tilesToChange--;
                    tiles[currentX, currentY].GroundType = groundToPlace;
                    if (tilesToChange <= 0)
                    {
                        return;
                    }
                }
                currentY -= 1;
                if (currentY < 0) { return; }
            }

            // left movement
            for (int left = 0; left < tilesToMove; left++)
            {
                if (Random.Range(0.0f, 1.0f) <= changePercent)
                {
                    tilesToChange--;
                    tiles[currentX, currentY].GroundType = groundToPlace;
                    if (tilesToChange <= 0)
                    {
                        return;
                    }
                }
                currentX -= 1;
                if (currentX < 0) { return; }
            }

            // up movement
            for (int up = 0; up < tilesToMove; up++)
            {
                if (Random.Range(0.0f, 1.0f) <= changePercent)
                {
                    tilesToChange--;
                    tiles[currentX, currentY].GroundType = groundToPlace;
                    if (tilesToChange <= 0)
                    {
                        return;
                    }
                }
                currentY += 1;
                if (currentY > height) { return; }
            }

            // right movement
            for (int right = 0; right < tilesToMove; right++)
            {
                if (Random.Range(0.0f, 1.0f) <= changePercent)
                {
                    tilesToChange--;
                    tiles[currentX, currentY].GroundType = groundToPlace;
                    if (tilesToChange <= 0)
                    {
                        return;
                    }
                }
                currentX += 1;
                if (currentX > width) { return; }
            }
        }
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
