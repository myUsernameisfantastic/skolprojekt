using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    // The movement range of this unit
    public int move = 3;

    public bool canTakeAction;

    void FixedUpdate()
    {
        if (!canTakeAction)
            GetComponent<SpriteRenderer>().color = new Color(0.43f, 0.43f, 0.43f, 1f);

        else
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

}
