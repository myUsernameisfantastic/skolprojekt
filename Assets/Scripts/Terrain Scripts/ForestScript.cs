using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a child of the TerrainScript class
public class ForestScript : TerrainScript {

    // The weight this type of tile holds
    // Is used in calculating movement ranges
    [SerializeField]
    private int weight = 2;
    public int Weight { get { return weight; } }

}
