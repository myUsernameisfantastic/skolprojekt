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

        Debug.Log("gm.playerUnits.Count = "+gm.playerUnits.Count);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TurnChange();
	}

    private void TurnChange()
    {
        if (playerTurn)
        {
            foreach (GameObject unit in gm.playerUnits)
            {
                if (unit.GetComponent<UnitScript>().canTakeAction)
                {
                    return;
                }

                else
                    playerTurn = false;
            }
        }

        else
        {
            foreach (GameObject unit in gm.enemyUnits)
            {
                if (unit.GetComponent<UnitScript>().canTakeAction)
                {
                    return;
                }

                else
                    playerTurn = true;
            }
        }
    }

    private void RefreshAction(List<GameObject> units)
    {
        foreach (GameObject unit in units)
        {
            unit.GetComponent<UnitScript>().canTakeAction = true;
        }
    }

}
