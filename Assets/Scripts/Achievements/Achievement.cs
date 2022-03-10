using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public string Name { get; }
    public string Description { get; }
    private bool unlocked = false;

    private List<AchievementProperty> properties;

    public Achievement(string name, string description)
    {
        this.Name = name;
        this.Description = description;
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

    public bool HasProgress()
    {
        bool hasProgress = true;
        foreach (var property in properties)
        {
            if (property.IsBoolean())
            {
                hasProgress = false;
                break;
            }
        }

        return hasProgress;
    }

    public string ProgressAsString()
    {
        string result = "";
        foreach (var property in properties)
        {
            if (!property.IsBoolean())
            {
                result += "(" + property.GetProgress() + ")";
            }
        }
        return result;
    }
}