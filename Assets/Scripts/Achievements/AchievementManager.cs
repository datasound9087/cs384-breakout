using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AchievementManager : MonoBehaviour
{
    private Dictionary<string, AchievementProperty> achievementProperties;
    private List<Achievement> achievements;

    void Awake()
    {
        LoadAchievements();

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnLevelComplete += this.OnLevelComplete;

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameOver += this.OnGameOver;
    }
    public void LoadAchievements()
    {
        achievementProperties = AchievementLoader.LoadProperties();
        achievements = AchievementLoader.LoadAchievements(achievementProperties);
    }

    public void OnLevelComplete(string levelName)
    {
        if (levelName == "Level01Complete")
        {
            achievementProperties["Level01Complete"].Value = 1;
        }
        
        CheckAchievements();
    }

    public void OnGameOver()
    {
        achievementProperties["GameOver"].Value = 1;
        CheckAchievements();
    }

    private void CheckAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.Check();
        }
    }
}
