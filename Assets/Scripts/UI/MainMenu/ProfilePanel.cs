using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfilePanel : MonoBehaviour 
{
    public GameObject profilesSelectionPanel;
    public GameObject profile1Button;
    public GameObject profile1LevelsHighScoreText;
    public GameObject profile1EndlessHighScoreText;
    public GameObject profile2Button;
    public GameObject profile2LevelsHighScoreText;
    public GameObject profile2EndlessHighScoreText;
    public GameObject profile3Button;
    public GameObject profile3LevelsHighScoreText;
    public GameObject profile3EndlessHighScoreText;
    public GameObject profile4Button;
    public GameObject profile4LevelsHighScoreText;
    public GameObject profile4EndlessHighScoreText;

    private MainMenu mainMenu;
    private ProfileCreationPanel profileCreationPanel;

    private const string LevelsHighScoreText = "Levels: ";
    private const string EndlessHighScoreText = "Endless: ";
    private const int MaxNumberOfProfiles = 4;

    void Awake()
    {
        mainMenu = GetComponent<MainMenu>();
        profileCreationPanel = GetComponent<ProfileCreationPanel>();
    }

    public void LoadProfiles()
    {
        PopulateProfiles();
    }

    public void Show()
    {
        profilesSelectionPanel.SetActive(true);
    }

    public void OnProfileButtonClicked(int profileIndex)
    {
        if (!ProfileExists(profileIndex))
        {
            ShowProfileCreationPanel();
        } else
        {
            ProfileManager.Instance.SetActiveProfile(profileIndex - 1);
            ReturnToMainMenu();
        }
    }

    private void ShowProfileCreationPanel()
    {
        profilesSelectionPanel.SetActive(false);
        profileCreationPanel.Show();
    }

    private bool ProfileExists(int profileNum)
    {
        return profileNum <= ProfileManager.Instance.ProfileCount();
    }

    private void PopulateProfiles()
    {
        List<Profile> profiles = ProfileManager.Instance.GetProfiles();
        if (profiles.Count == 0)
        {
            return;
        }

        // Trick for populating profiles without loads of duplicated code
        // make possible to reference related gameobjects via an index, then load all at once
        GameObject[] profileButtons = new GameObject[] {
            profile1Button, profile2Button,
            profile3Button, profile4Button
        };

        GameObject[] profileLevelsText = new GameObject[] {
            profile1LevelsHighScoreText, profile2LevelsHighScoreText,
            profile3LevelsHighScoreText, profile4LevelsHighScoreText
        };

        GameObject[] profileEndlessText = new GameObject[] {
            profile1EndlessHighScoreText, profile2EndlessHighScoreText,
            profile3EndlessHighScoreText, profile4EndlessHighScoreText
        };

        for (int i = 0; i < MaxNumberOfProfiles; i++)
        {
            if (i == profiles.Count)
            {
                break;
            }
            Profile profile = profiles[i];
            profileButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(profile.name);
            PopulateScoresForProfile(profile, profileLevelsText[i], profileEndlessText[i]);
        }
    }

    private void PopulateScoresForProfile(Profile profile, GameObject levelsText, GameObject endlessText)
    {
        levelsText.GetComponent<TextMeshProUGUI>().SetText(LevelsHighScoreText + profile.levelsHighScore);
        endlessText.GetComponent<TextMeshProUGUI>().SetText(EndlessHighScoreText + profile.endlessHighScore);
    }

    private void ReturnToMainMenu()
    {
        profilesSelectionPanel.SetActive(false);
        mainMenu.Show();
    }
}