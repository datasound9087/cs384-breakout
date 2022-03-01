using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AchievementManager : MonoBehaviour
{
    private Dictionary<string, AchievementProperty> achievementProperties;
    private List<Achievement> achievements;

    void Awake()
    {
        achievementProperties = AchievementLoader.LoadProperties();
        achievements = AchievementLoader.LoadAchievements(achievementProperties);
    }
}
