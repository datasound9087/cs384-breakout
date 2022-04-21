using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The structure of an achievement property in profile JSON.
*/
[System.Serializable]
public class AchievedPropertiesJSON
{
    public string propertyName;
    public int value;

    public AchievedPropertiesJSON(string propertyName, int value)
    {
        this.propertyName = propertyName;
        this.value = value;
    }
}