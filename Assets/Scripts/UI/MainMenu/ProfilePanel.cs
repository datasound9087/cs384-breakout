using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfilePanel : MonoBehaviour 
{
    public SoundManager soundManager;
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
    private const string DefaultProfileText = "Profile ";
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
        soundManager.PlaySound("MenuClick");
        if (!ProfileExists(profileIndex))
        {
            ShowProfileCreationPanel();
        } else
        {
            ProfileManager.Instance.SetActiveProfile(profileIndex - 1);
            if (EditProfile())
            {
                profileCreationPanel.SetProfileData(ProfileManager.Instance.GetActiveProfile());
                ShowProfileCreationPanel();
            } else if(DeleteProfile())
            {
                ProfileManager.Instance.DeleteProfile(profileIndex - 1);
                UpdateUIForProfile(profileIndex, DefaultProfileText, "", "");
            } else
            {
                ReturnToMainMenu();
            }
        }
    }

    private bool EditProfile()
    {
        return Input.GetKey("space");
    }

    private bool DeleteProfile()
    {
        return Input.GetKey("delete");
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
            profileButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = profile.name;
            PopulateScoresForProfile(profile, profileLevelsText[i], profileEndlessText[i]);
        }
    }

    private void PopulateScoresForProfile(Profile profile, GameObject levelsText, GameObject endlessText)
    {
        levelsText.GetComponent<TextMeshProUGUI>().text = LevelsHighScoreText + profile.levelsHighScore;
        endlessText.GetComponent<TextMeshProUGUI>().text = EndlessHighScoreText + profile.endlessHighScore;
    }

    private void UpdateUIForProfile(int profileIndex, string profileNameText, string levelsText, string endlessText)
    {
        // Trick for populating profiles without loads of duplicated code
        // make possible to reference related gameobjects via an index, then load all at once
        GameObject[] profileButtons = new GameObject[] {
            profile1Button, profile2Button,
            profile3Button, profile4Button
        };

        GameObject[] profileLevelsTexts = new GameObject[] {
            profile1LevelsHighScoreText, profile2LevelsHighScoreText,
            profile3LevelsHighScoreText, profile4LevelsHighScoreText
        };

        GameObject[] profileEndlessTexts = new GameObject[] {
            profile1EndlessHighScoreText, profile2EndlessHighScoreText,
            profile3EndlessHighScoreText, profile4EndlessHighScoreText
        };

        profileButtons[profileIndex - 1].GetComponentInChildren<TextMeshProUGUI>().text = profileNameText;
        profileLevelsTexts[profileIndex - 1].GetComponent<TextMeshProUGUI>().text = levelsText;
        profileEndlessTexts[profileIndex - 1].GetComponent<TextMeshProUGUI>().text = endlessText;
    }

    private void ReturnToMainMenu()
    {
        profilesSelectionPanel.SetActive(false);
        mainMenu.Show();
    }
}