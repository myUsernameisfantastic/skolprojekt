using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    // The movement range of this unit
    [SerializeField]
    private int move = 3;
    public int Move { get { return move; } }

    private bool canTakeAction;
    public bool CanTakeAction { get { return canTakeAction; } set { canTakeAction = value; } }

    void Update()
    {
        if (!canTakeAction)
            GetComponent<SpriteRenderer>().color = new Color(0.43f, 0.43f, 0.43f, 1f);

        else
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

}
