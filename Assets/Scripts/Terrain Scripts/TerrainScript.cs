using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {

    private int xPos;
    private int yPos;
    private int weight;
    //public bool occupied;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        }

        if (collision.CompareTag("Cursor"))
        {
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        }
    }

    public void IsItOccupied()
    {
    }
}
