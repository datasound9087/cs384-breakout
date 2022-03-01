using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AchievementJSON
{
    public string name;
    public string description;
    public bool persistsAcrossLevels;
    public List<string> properties;
}