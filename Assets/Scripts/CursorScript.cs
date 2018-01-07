using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    // Variables
    private Vector3 mousePos;
    private Vector3 newMousePos;
    // Object reference to the Rigidbody2D of this cursor
    private Rigidbody2D rb2d;
    // Object reference to the MoveManager script
    [SerializeField]
    private MoveManager moveManager;

    // Start
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    // FixedUpdate
    void FixedUpdate()
    {
        // Moves the cursor
        MoveCursor();

        // If the user clicks
        if (Input.GetMouseButtonDown(0))
        {
            // If the user clicked on a unit
            if (SelectUnit())
            {
                // Call the InitiateMove-method in MoveManager
                moveManager.InitiateMove();
            }
        }            
    }

    // Set the position of the cursor to the mouse position and snap to the grid
    private void MoveCursor()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        // Converts mouse position from pixel units to world units
        newMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -2f));
        // Moves the cursor to the mouse position coordinates rounded down
        rb2d.MovePosition(new Vector2(Mathf.Floor(newMousePos.x), Mathf.Floor(newMousePos.y)));
    }

    // Does a raycast to see if the user clicked on a unit
    private bool SelectUnit()
    {
        RaycastHit2D hit = Physics2D.Raycast(newMousePos, Vector2.zero);

        if (hit.transform.CompareTag("Player"))
        {
            // Set the selectedUnit object in MoveManager to the unit hit by the raycast
            moveManager.selectedUnit = hit.transform.gameObject;

            return true;
        }

        // If the raycast didn't hit anything: return false
        else return false;
    }
}

