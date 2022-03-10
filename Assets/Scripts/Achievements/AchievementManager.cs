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
        achievementProperties = AchievementIO.LoadProperties(ProfileManager.Instance.GetActiveProfile());
        achievements = AchievementIO.LoadAchievements(achievementProperties);
    }

    public void OnLevelComplete(string levelName)
    {
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
        foreach (var property in achievementProperties)
        {
            // Only reset if property has not yet been achieved
            if (!property.Value.PersistsAccrossLevels && !property.Value.IsActivated())
            {
                property.Value.Reset();
            }
        }
    }

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
        ProfileManager.Instance.SaveProfiles();
    }

    private void RegisterListeners()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnLevelComplete += this.OnLevelComplete;

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameOver += this.OnGameOver;
    }
}
