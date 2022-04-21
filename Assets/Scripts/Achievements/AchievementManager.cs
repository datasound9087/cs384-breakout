using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to manage achievment progress from game.
*/
public class AchievementManager : MonoBehaviour
{
    // Loaded achievement properties and achievements
    private Dictionary<string, AchievementProperty> achievementProperties;
    private List<Achievement> achievements;

    private void Awake()
    {
        LoadAchievements();
        RegisterListeners();
    }

    public void LoadAchievements()
    {
        // Load achievements and progress for the active profile
        achievementProperties = AchievementIO.LoadProperties(ProfileManager.Instance.GetActiveProfile());
        achievements = AchievementIO.LoadAchievements(achievementProperties);
    }

    public void OnLevelComplete(string levelName)
    {
        // Level complete - update related property
        if (levelName == "Level01Complete")
        {
            achievementProperties["Level01Complete"].Value = 1;
        }
    }

    public void OnGameOver()
    {
        achievementProperties["GameOver"].Value = 1;
    }

    public void OnBrickHit()
    {
        achievementProperties["HitABrick"].Value = 1;
        achievementProperties["Hit100Bricks"].Value++;
    }

    public void OnBrickDestroy()
    {
        achievementProperties["Destroy100Bricks"].Value++;
    }

    public void Reset()
    {
        // Reset non-persistent achievements between games/deaths
        foreach (var property in achievementProperties)
        {
            // Only reset if property has not yet been achieved
            if (!property.Value.PersistsAccrossLevels && !property.Value.IsActivated())
            {
                property.Value.Reset();
            }
        }
    }

    // Save the users achievement progress to disk
    public void Save()
    {
        Profile profile = ProfileManager.Instance.GetActiveProfile();
        foreach (var property in achievementProperties)
        {
            if (property.Value.PersistsAccrossLevels)
            {
                profile.StoreAchivementProperty(property.Value);
            }
            else
            {
                if (property.Value.IsActivated())
                {
                    profile.StoreAchivementProperty(property.Value);
                }
            }
        }
        
        // Save progress to disk
        ProfileManager.Instance.SaveProfiles();
    }

    private void RegisterListeners()
    {
        // Register listeners so can update achievemnt progress throughout game.
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnLevelComplete += this.OnLevelComplete;

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameOver += this.OnGameOver;
    }
}
