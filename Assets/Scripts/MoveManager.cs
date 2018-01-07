using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour {

    // struct used to hold coordinates and a bool for whether the tile at said coordinates is reachable by the selected unit or not
    public struct Coords
    {
        public int x, y;
        public bool legal;

        public Coords(int x, int y, bool legal)
        {
            this.x = x;
            this.y = y;
            this.legal = legal;
        }
    }

    // Object reference(?) to the MapManager script
    [SerializeField]
    private MapManager mapManager;

    // Checks to see if a given coordinate is outside the map boundaries
    private System.Predicate<int> OutsideMapRange = (x) => x <= -1 || x >= 5;

    // Array of Coords. Is used to check if a tile can be reached legitimately by a unit
    private Coords[,] legalMoves = new Coords[5, 5];

    // Bool that checks if a unit is in the process of moving
    private bool moveInMotion = false;

    // Object reference to the unit the user clicks on
    [HideInInspector]
    public GameObject selectedUnit;

    // FixedUpdate
    void FixedUpdate ()
    {
        // If a unit is in the process of moving and the user clicks
        if (moveInMotion && Input.GetMouseButtonDown(0))
        {
            // Set moveInMotion to false. Whether or not the unit actually moves does not matter
            moveInMotion = false;
            // Was manually set to false in InitiateMove()
            // Is manually set to true in case the unit doesn't move
            // (If the unit moves it will be set to false by OnTriggerExit2D)
            mapManager.mapTiles[(int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y].tile.GetComponent<TerrainScript>().occupied = true;           
            AttemptMove();
            DisplayMoveRange(new Color(1f, 1f, 1f, 1f));
            // Clears the legalMoves array
            System.Array.Clear(legalMoves, 0, legalMoves.Length);
        }
    }

    // Initiates the "process" of moving
    public void InitiateMove()
    {
        // Get the move-variable and the position of the selected unit
        int moveRange = selectedUnit.GetComponent<UnitScript>().move;
        int xPos = (int)selectedUnit.transform.position.x;
        int yPos = (int)selectedUnit.transform.position.y;

        // Set the occupied-variable of the tile the unit is standing on to false
        // (So that the unit can "move" to the tile it is already on)
        mapManager.mapTiles[xPos, yPos].tile.GetComponent<TerrainScript>().occupied = false;

        // Call CheckMoveRange() and
        // add 1 to the unit's move-variable because I'm too lazy to implement a flawless algorithm 
        CheckMoveRange(moveRange+1, xPos, yPos);

        // Call DisplayMoveRange()
        DisplayMoveRange(new Color(0.44f, 0.6f, 1f, 1f));   // #6F99FFFF (I think)

        // Set moveInMotion to true to allow the if-statement in FixedUpdate() to be reached(?)
        moveInMotion = true;
    }

    // Implementaion (sort of?) of a flood-fill
    // Decides whether a tile on the map is legitimately reachable by the unit or not
    // Takes an int for how many steps the unit can take and two int:s for the x and y position of a tile
    public void CheckMoveRange(int moveRange, int xPos, int yPos)
    {
        // If the given position is outside of the map: return
        if (OutsideMapRange.Invoke(xPos) || OutsideMapRange.Invoke(yPos))
            return;

        // Subtract the tile-weight from the remaing amount of steps
        int movesLeft = moveRange - mapManager.mapTiles[xPos, yPos].weight; 

        // If the remaining movement allows the unit to move onto the tile
        if (movesLeft >= 0)
        {
            // If the tile isn't occupied by another unit
            if (!mapManager.mapTiles[xPos, yPos].tile.GetComponent<TerrainScript>().occupied)
                // Add the position of this tile to the legalMoves array and set its legal-variable to true
                legalMoves[xPos, yPos] = new Coords(xPos, yPos, true);

            // This function is recursive
            CheckMoveRange(movesLeft, xPos + 1, yPos);
            CheckMoveRange(movesLeft, xPos - 1, yPos);
            CheckMoveRange(movesLeft, xPos, yPos - 1);
            CheckMoveRange(movesLeft, xPos, yPos + 1);
        }

        // If the remaining movement does not allow for the unit to move further: return
        else return;
    }

    // Sets the color of all the tiles the unit can move to to a different color
    private void DisplayMoveRange(Color color)
    {
        foreach (Coords legalMove in legalMoves)
        {
            if (legalMove.legal)
                mapManager.mapTiles[legalMove.x, legalMove.y].tile.GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Attempts to move the unit
    public void AttemptMove()
    {
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -2f));

        int mousePosX = (int)Mathf.Floor(newMousePos.x);
        int mousePosY = (int)Mathf.Floor(newMousePos.y);

        // If the position isn't outside the boundaries of the map
        if (!OutsideMapRange.Invoke(mousePosX) && !OutsideMapRange.Invoke(mousePosY))
        {
            // If [the tile at the position of the mouse at the time of clicking] is [a legal move]
            if (legalMoves[mousePosX, mousePosY].legal)
            {
                // Move the selected unit to said tile
                selectedUnit.GetComponent<Rigidbody2D>().MovePosition(new Vector2(mousePosX, mousePosY));
            }
        }

        else return;
    }
}
