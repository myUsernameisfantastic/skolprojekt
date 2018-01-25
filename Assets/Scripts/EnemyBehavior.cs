using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField]
    private MoveManager move;
    [SerializeField]
    private TurnManager turn;
    [SerializeField]
    private GameManager gm;
	
	void Update ()
    {
		if (!turn.playerTurn)
        {
            foreach (UnitScript unit in gm.enemyUnits)
            {
                move.selectedUnit = unit.gameObject;

                move.InitiateMove(false);

                while (unit.canTakeAction)
                {
                    int xRng = (int)Mathf.Floor(Random.Range(0f, 4f));
                    int yRng = (int)Mathf.Floor(Random.Range(0f, 4f));

                    if (move.legalMoves[xRng, yRng].legal)
                    {
                        // Moves the unit to a random legal tile
                        unit.GetComponent<Rigidbody2D>().MovePosition(new Vector2(xRng, yRng));
                        unit.canTakeAction = false;
                    }
                }

                System.Array.Clear(move.legalMoves, 0, move.legalMoves.Length);
            }

            // Was manually set to false in InitiateMove()
            // Is manually set to true in case the unit doesn't move
            // (If the unit moves it will be set to false by the OnTriggerExit2D on the tile object)
            //mapManager.mapTiles[(int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y].tile.GetComponent<TerrainScript>().occupied = true;

            turn.playerTurn = true;
            turn.RefreshAction(gm.playerUnits);
        }
	}

}
