﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    private bool playerTurn;
    public bool PlayerTurn { get { return playerTurn; } set { playerTurn = value; } }

    [SerializeField]
    private GameManager gm; 

	// Use this for initialization
	void Start()
    {
        playerTurn = true;

        RefreshAction(true);
    }
	
	// Update is called once per frame
	void Update()
    {
       // TurnChange();
	}

    private void TurnChange()
    {
        // If it is the player's turn
        if (playerTurn)
        {
            foreach (UnitScript unit in gm.PlayerUnits)
            {
                // If there is a unit who can take an action, return false
                if (unit.GetComponent<UnitScript>().CanTakeAction)
                {
                    return;
                }
            }

            playerTurn = false;
            RefreshAction(false);
        }
    }

    public void RefreshAction(bool isPlayer)
    {
        List<UnitScript> units = isPlayer ? gm.PlayerUnits : gm.EnemyUnits;

        foreach (UnitScript unit in units)
        {
            unit.GetComponent<UnitScript>().CanTakeAction = true;
        }
    }

}
