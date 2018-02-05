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
        // If it is the enemies' turn
		if (!turn.PlayerTurn)
        {
            List<UnitScript> units = gm.EnemyUnits;

            foreach (UnitScript unit in units)
            {
                move.SelectedUnit = unit.gameObject;

                move.InitiateMove(false);

                while (unit.CanTakeAction)
                {
                    int xRng = (int)Mathf.Floor(Random.Range(0f, 4f));
                    int yRng = (int)Mathf.Floor(Random.Range(0f, 4f));

                    if (move.LegalMoves[xRng, yRng].Legal)
                    {
                        // Moves the unit to a random legal tile
                        unit.GetComponent<Rigidbody2D>().MovePosition(new Vector2(xRng, yRng));
                        unit.CanTakeAction = false;
                    }
                }

                System.Array.Clear(move.LegalMoves, 0, move.LegalMoves.Length);
            }

            // Was manually set to false in InitiateMove()
            // Is manually set to true in case the unit doesn't move
            // (If the unit moves it will be set to false by the OnTriggerExit2D on the tile object)
            //mapManager.mapTiles[(int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y].tile.GetComponent<TerrainScript>().occupied = true;

            turn.PlayerTurn = true;
            turn.RefreshAction(true);
        }
	}

}
