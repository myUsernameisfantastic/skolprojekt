using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrain {

    int xPos { get; set; }
    int yPos { get; set; }
    int weight { get; set; }
    bool occupied { get; set; }
}
