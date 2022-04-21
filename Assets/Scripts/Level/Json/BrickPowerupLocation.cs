using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Brick powerup location data in Json.
*/
[System.Serializable]
public class BrickPowerupLocation
{
    // The name of the powerup (must match the name in powerups.json)
    public string name;

    // The bricks location in the level (0,0 is top left)
    public List<int> location;

    // Should powerup spawn on brick hit
    public bool onHit;

    // Should powerup spawn on brick durability change
    public int onDurability;
}