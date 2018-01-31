using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a child of teh TerrainScript class
public class MountainScript : TerrainScript {

    // The weight this type of tile holds
    // Is used in calculating movement ranges
    [SerializeField]
    private int weight = 4;
    public int Weight { get { return weight; } }

}
