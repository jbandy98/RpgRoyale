using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public Sprite[] groundSprites;
    public int width;
    public int height;

    static World world;

	// Use this for initialization
	void Start () {
        world = new World(width, height);

        // Create a GameObject for each of our tiles, so they show visually.
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                // Get the tile data
                Tile tile = world.GetTileAt(x, y);

                // This creates a new GameObject to pair with the tile data and adds it to our scene.
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile.X, tile.Y, 0);
                tile_go.transform.SetParent(this.transform);

                tile_go.AddComponent<SpriteRenderer>();
                OnTileTypeChanged(tile, tile_go);

                // Use a lambda to create an anonymous function to "wrap" our callback function
                tile.RegisterTileTypeChangedCallback((tiledata) => { OnTileTypeChanged(tiledata, tile_go); });
            }
        }
    }

    // This function should be called automatically whenever a tile's type gets changed.
    void OnTileTypeChanged(Tile tile, GameObject tile_go)
    {
        if (tile.GroundType == GroundType.EMPTY)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        } else 
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = groundSprites[(int)tile.GroundType];
        }
    }

    public static World World
    {
        get
        {
            return world;
        }
    }

}
