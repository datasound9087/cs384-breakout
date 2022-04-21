using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Achievement UI Handler.
*/
public class AchievementsMenu : MonoBehaviour
{
    // achievemnt scroll window
    public GameObject scrollViewport;
    public GameObject achievementPrefab;
    public Color achievedTextColour;
    public SoundManager soundManager;

    private MenuSceneAnimator menuSceneAnimator;

    private void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
        AchievementManager achievementManager = GetComponent<AchievementManager>();
        LoadAchievements();
    }

    public void OnBackButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    private void LoadAchievements()
    {   
        // Get achievement progress for the active profile
        List<Achievement> achievements = AchievementIO.LoadPropertiesAndAchievements(ProfileManager.Instance.GetActiveProfile());
        PopulateScrollView(achievements);
    }

    private void PopulateScrollView(List<Achievement> achievements)
    {
        foreach (Achievement achievement in achievements)
        {
            // Initialise Prefab
            GameObject ach = Instantiate(achievementPrefab, scrollViewport.transform, false);

            // Set text details
            TextMeshProUGUI nameText = GetPrefabTextComponent(ach, "Name");
            TextMeshProUGUI descriptionText = GetPrefabTextComponent(ach, "Description");

            // If already unlocked, set achieved colour and append Achieved to end
            string achievementTitle = achievement.Name;
            if (achievement.Unlocked())
            {
                nameText.color = achievedTextColour;
                descriptionText.color = achievedTextColour;
                achievementTitle += " (Achieved)"; 
            }
            else
            {
                // If achievemnt has associated progress display it
                if (achievement.HasProgress())
                {
                    achievementTitle += " " + achievement.ProgressAsString();
                }
            }

            nameText.text = achievementTitle;
            descriptionText.text = achievement.Description;
        }
    }

    private TextMeshProUGUI GetPrefabTextComponent(GameObject prefab, string name)
    {
        TextMeshProUGUI textComponent = null;
        GameObject go = prefab.transform.Find(name).gameObject;
        textComponent = go.GetComponent<TextMeshProUGUI>();
        return textComponent;
    }
}
