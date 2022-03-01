using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public string Name { get; }
    public string Description { get; }
    public bool PersistsAcrossLevels { get; }
    private bool unlocked = false;

    private List<AchievementProperty> properties;

    public Achievement(string name, string description, bool persistsAcrossLevels)
    {
        this.Name = name;
        this.Description = description;
        this.PersistsAcrossLevels = persistsAcrossLevels;
        this.properties = new List<AchievementProperty>();
    }


    public void addProperty(AchievementProperty property)
    {
        if (!properties.Contains(property))
        {
            properties.Add(property);
        }
    }

    public bool Unlocked()
    {
        return unlocked;
    }

    public void Check()
    {
        bool achieved = true;
        foreach (AchievementProperty property in properties)
        {
            if (!property.IsActivated())
            {
                achieved = false;
                break;
            }
        }

        if (achieved)
        {
            unlocked = true;
        }
    }
}