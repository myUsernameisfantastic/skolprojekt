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

    // Checks to see if a given coordinate is outside the amp boundaries
    private System.Predicate<int> OutsideMapRange = (x) => x == -1 || x == 5;

    public bool okCollision = false;

    public GameObject selectedUnit;

    [SerializeField]
    private MapManager mapManager;

    private Coords[,] legalMoves = new Coords[5, 5];



    // 
    public void AttemptMove()
    {
        int moveRange = selectedUnit.GetComponent<UnitScript>().move;
        int xPos = (int)selectedUnit.transform.position.x;
        int yPos = (int)selectedUnit.transform.position.y;

        // add 1 to moveRange because I'm too lazy to implement a flawless algorithm 
        CheckMoveRange(moveRange+1, xPos, yPos);

        InitiateMove();
    }


    //
    public void CheckMoveRange(int moveRange, int xPos, int yPos)
    {
        if (OutsideMapRange.Invoke(xPos) || OutsideMapRange.Invoke(yPos))
            return;

        int movesLeft = moveRange - mapManager.mapTiles[xPos, yPos].weight; //weightedMap

        // If the unit's movement allows it to move into the square, and if the square isn't already occupied
        if (movesLeft >= 0)
        {
            legalMoves[xPos, yPos] = new Coords(xPos, yPos, true);

            CheckMoveRange(movesLeft, xPos + 1, yPos);
            CheckMoveRange(movesLeft, xPos - 1, yPos);
            CheckMoveRange(movesLeft, xPos, yPos - 1);
            CheckMoveRange(movesLeft, xPos, yPos + 1);
        }

        else return;
    }


    //
    public void InitiateMove()
    {
        // #6F99FFFF
        foreach (Coords legalMove in legalMoves)
        {
            if (legalMove.legal)
            {
                mapManager.mapTiles[legalMove.x, legalMove.y].tile.GetComponent<SpriteRenderer>().color = new Color(0.44f, 0.60f, 1f, 1f); //remove tile
            }
        }
    }
}
