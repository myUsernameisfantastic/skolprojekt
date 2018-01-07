using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestScript : MonoBehaviour {

    public int weight;
    public bool occupied;

    void Awake ()
    {
        occupied = false;
        weight = 2;
    }

    // Use this for initialization
    void Start ()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            occupied = true;
        }

        if (collision.CompareTag("Cursor"))
        {
            Debug.Log(occupied.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            occupied = false;
        }
    }
}
