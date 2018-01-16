using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class of all terrain types
public class TerrainScript : MonoBehaviour {

    // Bool used to check if the tile is occupied by a unit
    public bool occupied;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If a unit enters a tile, set occupied to true
        if (collision.GetComponent<UnitScript>() != null)
        {
            occupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If a unit exits the tile, set occupied to false
        if (collision.GetComponent<UnitScript>() != null)
        {
            occupied = false;
        }
    }
}
