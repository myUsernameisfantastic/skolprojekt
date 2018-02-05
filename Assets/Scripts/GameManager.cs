using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Arrays holding prefabs of units
    [SerializeField]
    private GameObject[][] unitPrefabs = new GameObject[2][];
    [SerializeField]
    private GameObject[] playerPrefabs = new GameObject[1];
    [SerializeField]
    private GameObject[] enemyPrefabs = new GameObject[1];

    // Lists of player units and enemy units
    private List<UnitScript> playerUnits;
    private List<UnitScript> enemyUnits;
    public List<UnitScript> PlayerUnits { get { return playerUnits; } }
    public List<UnitScript> EnemyUnits { get { return enemyUnits; } }

    // Use this for initialization
    void Start()
    {
        playerUnits = new List<UnitScript>();
        enemyUnits = new List<UnitScript>();

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
    }

    // Instantiates a unit and adds it to the appropriate List
    // also makes it a child of the GameManager
    private void SpawnUnit(GameObject prefab, Vector3 position, bool isPlayer)
    {
        GameObject instance = Instantiate(prefab, position, Quaternion.identity, transform) as GameObject;
        UnitScript unit = instance.GetComponent<UnitScript>();

        // Adds the unit to a list of units
        if (isPlayer)
            playerUnits.Add(unit);

        else
            enemyUnits.Add(instance.GetComponent<UnitScript>());
    }
}
