using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A profile representing a player.
*/
[System.Serializable]
public class Profile
{
    public string name;
    public int endlessHighScore;
    public int levelsHighScore;

    // Achievement progress
    public List<AchievedPropertiesJSON> achivementProperties;

    public Profile()
    {
        achivementProperties = new List<AchievedPropertiesJSON>();
    }

    // Store in an achievemnt property progress, update if already exists
    public void StoreAchivementProperty(AchievementProperty property)
    {
        int foundIndex = achivementProperties.FindIndex(json => json.propertyName == property.Name);
        if (foundIndex == -1)
        {
            // Not found, create it
            achivementProperties.Add(new AchievedPropertiesJSON(property.Name, property.Value));
        }
        else
        {
            achivementProperties[foundIndex].value = property.Value;
        }
    }
}