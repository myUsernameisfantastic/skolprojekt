using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    private Vector3 mousePos;
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
                moveManager.AttemptMove();
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

    private void MoveUnit()
    {
        /*if (moveManager.okCollision && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hej");
        }
        */
    }

    private bool SelectUnit()
    {
        RaycastHit2D hit = Physics2D.Raycast(newMousePos, Vector2.zero);

        if (hit.transform.CompareTag("Player"))
        {
            moveManager.selectedUnit = hit.transform.gameObject;

            return true;
        }

        else return false;
    }
}

