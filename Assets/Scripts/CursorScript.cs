using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    private Vector3 newMousePos;
    private Rigidbody2D rb2d;
    [SerializeField]
    private MoveManager moveManager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        MoveCursor();

        if (Input.GetMouseButtonDown(0))
        {
            if (SelectUnit())
            {
                moveManager.InitiateMove(true);
            }
        }            
    }

    // Set the position of the cursor to the mouse position and snap it to the grid
    private void MoveCursor()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        // Converts mouse position from pixel units to world units
        newMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -2f));
        // Moves the cursor to the mouse position coordinates rounded down
        rb2d.MovePosition(new Vector2((int)newMousePos.x, (int)newMousePos.y));
    }

    // Does a raycast to see if the user clicked on a unit
    private bool SelectUnit()
    {
        RaycastHit2D hit = Physics2D.Raycast(newMousePos, Vector2.zero);

        // If raycast hit a unit, set selectedUnit in moveManager to the unit that was hit and return true. Else return false.
        return (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Enemy")) ? moveManager.SelectedUnit = hit.transform.gameObject : false;
    }
}

