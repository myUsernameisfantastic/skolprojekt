using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public bool playerTurn;

    [SerializeField]
    private GameManager gm; 

	// Use this for initialization
	void Start ()
    {
        playerTurn = true;

        RefreshAction(gm.playerUnits);
    }
	
	// Update is called once per frame
	void Update ()
    {
        TurnChange();
	}

    private void TurnChange()
    {
        // If it is the player's turn
        if (playerTurn)
        {
            foreach (UnitScript unit in gm.playerUnits)
            {
                // If there is a unit who can take an action, return false
                if (unit.GetComponent<UnitScript>().canTakeAction)
                {
                    return;
                }
            }

            playerTurn = false;
            RefreshAction(gm.enemyUnits);
        }
    }

    public void RefreshAction(List<UnitScript> units)
    {
        foreach (UnitScript unit in units)
        {
            unit.GetComponent<UnitScript>().canTakeAction = true;
        }
    }

}
