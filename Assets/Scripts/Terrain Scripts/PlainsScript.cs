using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a child of the TerrainScript class
public class PlainsScript : TerrainScript {

    // The weight this type of tile holds
    // Is used in calculating movement ranges
    [SerializeField]
    private int weight = 1;
    public int Weight { get { return weight; } }

}
