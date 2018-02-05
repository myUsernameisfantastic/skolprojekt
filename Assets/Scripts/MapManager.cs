using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // struct that holds an object reference to a tile on the map, its position and its weight (and also which terrain-type it is)
    public struct TilesOfTheMap
    {
        private GameObject tile;
        private Component terrainType;
        private int x, y;
        private int weight;

        public GameObject Tile { get { return tile; } }
        public Component TerrainType { get { return terrainType; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Weight { get { return weight; } }

        public TilesOfTheMap(GameObject tile)
        {
            this.tile = tile;

            x = (int)tile.transform.position.x;
            y = (int)tile.transform.position.y;

            // Checks if the tile is a plainsTile, forestTile or mountainTile and sets the weight-variable accordingly
            // Also sets the corresponding terrainType
            if (tile.GetComponent<PlainsScript>() != null)
            {
                terrainType = tile.GetComponent<PlainsScript>();
                weight = tile.GetComponent<PlainsScript>().Weight;
            }

            else if (tile.GetComponent<ForestScript>() != null)
            {
                terrainType = tile.GetComponent<ForestScript>();
                weight = tile.GetComponent<ForestScript>().Weight;
            }

            else
            {
                terrainType = tile.GetComponent<MountainScript>();
                weight = tile.GetComponent<MountainScript>().Weight;
            }
        }
    }

    // How big the map is
    private int limitX = 5;
    private int limitY = 5;
    public int LimitX { get { return limitX; } }
    public int LimitY { get { return limitY; } }
    //Array holding MapTiles-structs
    private TilesOfTheMap[,] mapTiles = new TilesOfTheMap[5, 5];
    public TilesOfTheMap[,] MapTiles { get { return mapTiles; } }

    // Array holding the terrain tile prefabs
    // 0 = plains
    // 1 = forest
    // 2 = mountain
    [SerializeField]
    private GameObject[] tilePrefabs = new GameObject[3];

    // Awake
    void Awake()
    {
        CreateMap();
    }

    // Creates the map
    private void CreateMap()
    {
        // Fills the map with PlainsTile:s
        for (int y = 0; y < limitY; y++)
        {
            for (int x = 0; x < limitX; x++)
            {
                LayTile(tilePrefabs[0], new Vector3(x, y, 0f), 1);
            }
        }

        // Hardcoded tiles
        LayTile(tilePrefabs[1], new Vector3(3f, 1f, 0f), 2);
        LayTile(tilePrefabs[2], new Vector3(2f, 2f, 0f), 10);
    }
    
    // Instantiates a given tile at a given position
    private void LayTile(GameObject prefab, Vector3 position, int weight)
    {
        // If a tile already exists at the given position, destroy it
        if (mapTiles[(int)position.x, (int)position.y].Tile != null)
            Destroy(mapTiles[(int)position.x, (int)position.y].Tile);

        // Makes the instantiated tile a child of the map
        GameObject instance = Instantiate(prefab, position, Quaternion.identity, this.transform) as GameObject;

        // Adds the GameObject to the mapTiles-array, with the index corresponding to the tile's position in world space
        mapTiles[(int)position.x, (int)position.y] = new TilesOfTheMap(instance);
    }

}
