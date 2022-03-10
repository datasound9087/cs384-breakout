using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Profile
{
    public string name;
    public int endlessHighScore;
    public int levelsHighScore;

    public List<AchievedPropertiesJSON> achivementProperties;

    public Profile()
    {
        achivementProperties = new List<AchievedPropertiesJSON>();
    }
    public void StoreAchivementProperty(AchievementProperty property)
    {
        int foundIndex = achivementProperties.FindIndex(json => json.propertyName == property.Name);
        if (foundIndex == -1)
        {
            achivementProperties.Add(new AchievedPropertiesJSON(property.Name, property.Value));
        }
        else
        {
            achivementProperties[foundIndex].value = property.Value;
        }
    }
}