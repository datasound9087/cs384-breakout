using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Game Achievemnt.
*/
public class Achievement
{
    public string Name { get; }
    public string Description { get; }
    private bool unlocked = false;

    // The various properties that make up this achievment
    private List<AchievementProperty> properties;

    public Achievement(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.properties = new List<AchievementProperty>();
    }

    public void AddProperty(AchievementProperty property)
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

    // Has this achievement been unlocked - has all it's related properties been activated
    public void Check()
    {
        // Check each property
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

    // Does this achievement have progress - does it take more than a boolean activation to complete
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

    // Concatenate each properties progress to a string to be displayed
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