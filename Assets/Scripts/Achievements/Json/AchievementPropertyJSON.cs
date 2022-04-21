using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A property about the game that can be tracked.
 * An achievement consists of one or more of these properties
*/
[System.Serializable]
public class AchievementPropertyJSON
{
    // Name
    public string name;

    // Intitial value
    public int initialValue;

    // Value to activate at
    public int activationValue;

    // Rule to activate on (arithematic operator =, >, <, <=, >=)
    public string activationRule;

    // Does this property get reset between levels
    public bool persistsAcrossLevels;
}