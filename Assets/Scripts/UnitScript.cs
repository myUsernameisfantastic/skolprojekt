using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    // The movement range of this unit
    public int move = 3;

    // Object reference to the MoveManager script
    [SerializeField]
    private MoveManager moveManager;
}
