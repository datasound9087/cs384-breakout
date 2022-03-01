using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementLoader
{
    public static Dictionary<string, AchievementProperty> LoadProperties()
    {
        AchievementPropertiesJSON loadedPropertiesJSON = null;
        // Load json level file from disk into objects
        TextAsset ob = (TextAsset)Resources.Load("Achievements/AchievementProperties", typeof(TextAsset));
        loadedPropertiesJSON = JsonUtility.FromJson<AchievementPropertiesJSON>(ob.ToString());

        Dictionary<string, AchievementProperty> loadedProperties = null;
        if (loadedPropertiesJSON != null)
        {
            loadedProperties = new Dictionary<string, AchievementProperty>();
            ParseProperties(loadedPropertiesJSON.achievementProperties, loadedProperties);
        }

        return loadedProperties;
    }

    private static void ParseProperties(List<AchievementPropertyJSON> jsonProperties, Dictionary<string, AchievementProperty> loadedProperties)
    {
        foreach (AchievementPropertyJSON json in jsonProperties)
        {
            ActivationRule activationRule = ParseActivationRule(json.activationRule);
            AchievementProperty property = new AchievementProperty(json.name, json.initialValue, json.activationValue, activationRule);
            loadedProperties.Add(property.Name, property);
        }
    }

    private static ActivationRule ParseActivationRule(string value)
    {
        switch (value)
        {
            case "=" : return ActivationRule.EQUAL_TO;
            case "<" : return ActivationRule.LESS_THAN;
            case "<=": return ActivationRule.LESS_EQUAL_TO;
            case ">" : return ActivationRule.GREATER_THAN;
            case ">=" : return ActivationRule.GREATER_EQUAL_TO;
            default : return ActivationRule.EQUAL_TO; // Just in case there be an invalid rule
        }
    }

    public static List<Achievement> LoadAchievements(Dictionary<string, AchievementProperty> properties)
    {
        AchievementsJSON loadedAchievementsJSON = null;
        // Load json level file from disk into objects
        TextAsset ob = (TextAsset)Resources.Load("Achievements/Achievements", typeof(TextAsset));
        loadedAchievementsJSON = JsonUtility.FromJson<AchievementsJSON>(ob.ToString());
        
        List<Achievement> loadedAchievements = null;
        if (loadedAchievementsJSON != null)
        {
            loadedAchievements = new List<Achievement>();
            ParseAchievements(loadedAchievementsJSON.achievements, properties, loadedAchievements);
        }

        return loadedAchievements;
    }

    private static void ParseAchievements(List<AchievementJSON> jsonAchievements, Dictionary<string, AchievementProperty> properties, List<Achievement> loadedAchievements)
    {
        foreach (AchievementJSON json in jsonAchievements)
        {
            Achievement achievement = new Achievement(json.name, json.description, json.persistsAcrossLevels);
            AddAchievementProperties(achievement, json.properties, properties);
            loadedAchievements.Add(achievement);
        }
    }

    private static void AddAchievementProperties(Achievement achievement, List<string> achievementJsonProperties, Dictionary<string, AchievementProperty> properties)
    {
        foreach (string propertyName in achievementJsonProperties)
        {
            if (!properties.ContainsKey(propertyName))
            {
                continue;
            }

            AchievementProperty property = properties[propertyName];
            achievement.addProperty(property);
        }
    }
}