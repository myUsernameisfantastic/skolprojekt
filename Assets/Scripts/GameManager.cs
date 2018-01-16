using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Arrays holding prefabs of units
    private GameObject[][] unitPrefabs = new GameObject[2][];
    [SerializeField]
    private GameObject[] playerPrefabs = new GameObject[1];
    [SerializeField]
    private GameObject[] enemyPrefabs = new GameObject[1];

    // Lists of player units and enemy units
    
    public List<GameObject> playerUnits;
    [HideInInspector]
    public List<GameObject> enemyUnits;

    // Use this for initialization
    void Start ()
    {
        // first index 0 = player units
        // first index 1 = enemy units
        // second index 0 = knight prefab
        unitPrefabs[0] = playerPrefabs;
        unitPrefabs[1] = enemyPrefabs;

        // Hardcoded units
        SpawnUnit(unitPrefabs[0][0], new Vector3(1f, 1f, -1f), true);     // Creates a player knight
        SpawnUnit(unitPrefabs[0][0], new Vector3(2f, 1f, -1f), true);
        SpawnUnit(unitPrefabs[0][0], new Vector3(1f, 2f, -1f), true);
        SpawnUnit(unitPrefabs[1][0], new Vector3(3f, 3f, -1f), false);     // Creates an enemy knight
        SpawnUnit(unitPrefabs[1][0], new Vector3(4f, 2f, -1f), false);     // Creates another enemy knight
    }

    // Instantiates a unit and adds it to the appropriate List
    // also makes it a child of the GameManager
    private void SpawnUnit(GameObject prefab, Vector3 position, bool isPlayer)
    {
        GameObject instance = Instantiate(prefab, position, Quaternion.identity, transform) as GameObject;

        // If the unit is a player unit: add it to the player list
        if (isPlayer)
            playerUnits.Add(instance);

        // Else: add it to the enemy list
        else
            enemyUnits.Add(instance);
    }
}
