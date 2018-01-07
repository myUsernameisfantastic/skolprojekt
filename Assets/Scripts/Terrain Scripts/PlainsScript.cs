using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is a child of the TerrainScript class
public class PlainsScript : TerrainScript {

    // The weight this type of tile holds
    // Is used in calculating movement ranges
    public int weight;

    void Awake ()
    {
        weight = 1;
    }
}
