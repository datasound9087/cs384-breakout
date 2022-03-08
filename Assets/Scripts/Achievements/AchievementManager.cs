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
        RegisterListeners();
    }

    public void LoadAchievements()
    {
        achievementProperties = AchievementIO.LoadProperties();
        achievements = AchievementIO.LoadAchievements(achievementProperties);
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

    public void OnBrickHit()
    {
        achievementProperties["HitABrick"].Value = 1;
        CheckAchievements();
    }

    public void OnBrickDestroy()
    {
        CheckAchievements();
    }

    public void Reset()
    {
        foreach (var property in achievementProperties)
        {
            if (!property.Value.PersistsAccrossLevels)
            {
                property.Value.Reset();
            }
        }
    }

    private void CheckAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.Check();
        }
    }

    private void RegisterListeners()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnLevelComplete += this.OnLevelComplete;

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameOver += this.OnGameOver;
    }
}
