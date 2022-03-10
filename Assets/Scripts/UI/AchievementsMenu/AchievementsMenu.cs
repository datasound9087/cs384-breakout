using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AchievementsMenu : MonoBehaviour
{
    public GameObject scrollViewport;
    public GameObject achievementPrefab;
    public Color achievedTextColour;

    private MenuSceneAnimator menuSceneAnimator;

    void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();

        AchievementManager achievementManager = GetComponent<AchievementManager>();
        LoadAchievements();
    }
    public void OnBackButtonClicked()
    {
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    private void LoadAchievements()
    {
        List<Achievement> achievements = AchievementIO.LoadPropertiesAndAchievements();
        PopulateScrollView(achievements);
    }

    private void PopulateScrollView(List<Achievement> achievements)
    {
        foreach (Achievement achievement in achievements)
        {
            GameObject ach = Instantiate(achievementPrefab, scrollViewport.transform, false);

            TextMeshProUGUI nameText = GetPrefabTextComponent(ach, "Name");
            TextMeshProUGUI descriptionText = GetPrefabTextComponent(ach, "Description");

            if (achievement.Unlocked())
            {
                nameText.color = achievedTextColour;
                descriptionText.color = achievedTextColour;
            }

            nameText.text = achievement.Unlocked() ? achievement.Name + " (Achieved)": achievement.Name;
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
