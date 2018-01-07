using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    public int move = 3;

    [SerializeField]
    private MoveManager moveManager;

	// Use this for initialization
	void Start ()
    {		
	}
	
	void FixedUpdate ()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursor"))
            moveManager.okCollision = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursor"))
            moveManager.okCollision = false;
    }
}
