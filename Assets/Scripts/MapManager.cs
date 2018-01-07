using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // struct that holds an object reference to a tile on the map, its position and its weight
    public struct MapTiles
    {
        public GameObject tile;
        public int x, y;
        public int weight;

        public MapTiles(GameObject tile)
        {
            this.tile = tile;

            this.x = (int)tile.transform.position.x;
            this.y = (int)tile.transform.position.y;

            // Checks if the tile is a plainsTile, forestTile or mountainTile and sets  this weight to corresponding weight
            // Defaults to 1
            if (tile.GetComponent<PlainsScript>() != null)
                weight = tile.GetComponent<PlainsScript>().weight;

            else if (tile.GetComponent<ForestScript>() != null)
                weight = tile.GetComponent<ForestScript>().weight;

            else if (tile.GetComponent<MountainScript>() != null)
                weight = tile.GetComponent<MountainScript>().weight;

            else weight = 1;
        }
    }

    // Object reference to [the map]
    // (is parent of [the tiles])
    private Transform map;

    /*****************************************************************************************************
    // Array of the tiles making the map
    public GameObject[,] mapTiles = new GameObject[5, 5];
    // Array of the weight of the tiles
    public int[,] weightedMap = new int[5, 5];
    *****************************************************************************************************/

    //Array holding MapTiles-structs
    public MapTiles[,] mapTiles = new MapTiles[5, 5];

    // Array holding the terrain tile prefabs
    // 0 = plains
    // 1 = forest
    // 2 = mountain
    [SerializeField]
    private GameObject[] tilePrefabs = new GameObject[3];

    void Awake()
    {
        map = GetComponent<Transform>();

        CreateMap();
    }


    // Creates the map
    private void CreateMap()
    {
        // Places plainsTile:s within an area defined by the lengths of the for-loops
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                LayTile(tilePrefabs[0], new Vector3(x, y, 0f), 1);
            }
        }

        // Hardcoded tiles
        LayTile(tilePrefabs[1], new Vector3(3f, 1f, 0f), 2);
        LayTile(tilePrefabs[2], new Vector3(2f, 2f, 0f), 10);
    }

    /****************************************************************************************************
    // Instantiates a given tile at a given position
    private void LayTile(GameObject prefab, Vector3 position, int weight)
    {
        // If a tile already exists at the given position, destroy it
        if (mapTiles[(int)position.x, (int)position.y] != null)
            Destroy(mapTiles[(int)position.x, (int)position.y]);


        // Makes the instantiated tile a child of [the map]
        GameObject instance = Instantiate(prefab, position, Quaternion.identity, map) as GameObject;


        // Adds the GameObject to the mapTiles-array, with the index corresponding to the tile's position
        mapTiles[(int)position.x, (int)position.y] = instance;

        // Adds the weight value of the gameobject to the weightedMap-array
        weightedMap[(int)position.x, (int)position.y] = RecordWeight(instance);
        Debug.Log(RecordWeight(instance).ToString());
    }****************************************************************************************************/


    
    // Instantiates a given tile at a given position
    private void LayTile(GameObject prefab, Vector3 position, int weight)
    {
        // If a tile already exists at the given position, destroy it
        if (mapTiles[(int)position.x, (int)position.y].tile != null)
            Destroy(mapTiles[(int)position.x, (int)position.y].tile);

        // Makes the instantiated tile a child of [the map]
        GameObject instance = Instantiate(prefab, position, Quaternion.identity, map) as GameObject;

        // Adds the GameObject to the mapTiles-array, with the index corresponding to the tile's position in world space
        mapTiles[(int)position.x, (int)position.y] = new MapTiles(instance);
    }


    // Returns the weight value of the tile
    private int RecordWeight(GameObject tile)
    {
        // Checks if the tile is a plainsTile, forestTile or mountainTile and returns corresponding weight
        // Defaults to 1
        if (tile.GetComponent<PlainsScript>() != null)
            return tile.GetComponent<PlainsScript>().weight;

        else if (tile.GetComponent<ForestScript>() != null)
            return tile.GetComponent<ForestScript>().weight;

        else if (tile.GetComponent<MountainScript>() != null)
            return tile.GetComponent<MountainScript>().weight;

        else return 1;
    }
}
