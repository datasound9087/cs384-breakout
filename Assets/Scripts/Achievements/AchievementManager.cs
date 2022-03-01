using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AchievementManager
{
    private Dictionary<string, AchievementProperty> achievementProperties;
    private List<Achievement> achievements;

    public void Init()
    {
        achievementProperties = AchievementLoader.LoadProperties();
        achievements = AchievementLoader.LoadAchievements(achievementProperties);
    }

    public void OnNotify(GameObject gameObject, Event gameEvent)
    {
        /*switch (gameEvent)
        {
            case Event.LEVEL_COMPLETE
        }*/
    }
}
