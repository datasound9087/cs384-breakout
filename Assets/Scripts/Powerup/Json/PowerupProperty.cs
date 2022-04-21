using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A powerup as defined in JSON.
*/
[System.Serializable]
public class PowerupProperty
{
    // Name of powerup
    public string name;

    // Duration in seconds
    public int duration;
}
