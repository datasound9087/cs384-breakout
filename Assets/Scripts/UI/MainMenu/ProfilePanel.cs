using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Profile Selection UI handler.
*/
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

    private void Awake()
    {
        // Get related UI handlers
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

    // Callback for all profile buttons - each has an index from 1 to 4
    public void OnProfileButtonClicked(int profileIndex)
    {
        soundManager.PlaySound("MenuClick");

        // Create profile if it does not exist
        if (!ProfileExists(profileIndex))
        {
            ShowProfileCreationPanel();
        } else
        {
            //set active profile based on index
            ProfileManager.Instance.SetActiveProfile(profileIndex - 1);
            if (EditProfile())
            {
                profileCreationPanel.SetProfileData(ProfileManager.Instance.GetActiveProfile());
                ShowProfileCreationPanel();
            } else if(DeleteProfile())
            {
                // Delete profile and update UI
                ProfileManager.Instance.DeleteProfile(profileIndex - 1);
                UpdateUIForProfile(profileIndex, DefaultProfileText, "", "");
            } else
            {
                ReturnToMainMenu();
            }
        }
    }

    // Edit mode if space is pressed simultaneusly
    private bool EditProfile()
    {
        return Input.GetKey("space");
    }

    // Delete profile if delete is pressed simultaneusly
    private bool DeleteProfile()
    {
        return Input.GetKey("delete");
    }

    private void ShowProfileCreationPanel()
    {
        profilesSelectionPanel.SetActive(false);
        profileCreationPanel.Show();
    }

    // As there can only be 4 profiles at a time, a profile can only exist if it is <= the number currently stored in ProfileManager
    // So if profile 4 clicked on but only 3 exist, 4 > 3 so profile does not exist
    private bool ProfileExists(int profileNum)
    {
        return profileNum <= ProfileManager.Instance.ProfileCount();
    }

    private void PopulateProfiles()
    {
        // No point trying to load no profiles :)
        List<Profile> profiles = ProfileManager.Instance.GetProfiles();
        if (profiles.Count == 0)
        {
            return;
        }

        // Trick for populating profiles without loads of duplicated code
        // makes it possible to reference related gameobjects via an index, then load all at once
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

        // Load each profile going left to right, top to bottom in UI
        // This way non existent profiles fill any remaining space
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

    // Update UI details for a profile
    private void PopulateScoresForProfile(Profile profile, GameObject levelsText, GameObject endlessText)
    {
        levelsText.GetComponent<TextMeshProUGUI>().text = LevelsHighScoreText + profile.levelsHighScore;
        endlessText.GetComponent<TextMeshProUGUI>().text = EndlessHighScoreText + profile.endlessHighScore;
    }

    private void UpdateUIForProfile(int profileIndex, string profileNameText, string levelsText, string endlessText)
    {
        // Trick for populating profiles without loads of duplicated code
        // makes it possible to reference related gameobjects via an index, then load all at once
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