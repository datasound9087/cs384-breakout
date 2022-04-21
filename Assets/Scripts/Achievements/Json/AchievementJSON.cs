using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * JSON representation of an achievement
*/
[System.Serializable]
public class AchievementJSON
{
    // Display name
    public string name;

    // Description
    public string description;

    // Linked achievement properties (must match with the names defined in AchievementProperties JSON)
    public List<string> properties;
}