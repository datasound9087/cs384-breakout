using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfilePanel : MonoBehaviour 
{
    public GameObject profilesSelectionPanel;
    public GameObject profile1Button;
    public GameObject profile2Button;
    public GameObject profile3Button;
    public GameObject profile4Button;

    private MainMenu mainMenu;
    private ProfileCreationPanel profileCreationPanel;

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

        if (profiles.Count >= 1)
        {
            profile1Button.GetComponentInChildren<TextMeshProUGUI>().SetText(profiles[0].name);
        }
        
        if (profiles.Count >= 2)
        {
            profile2Button.GetComponentInChildren<TextMeshProUGUI>().SetText(profiles[1].name);
        } 
        
        if (profiles.Count >= 3)
        {
            profile3Button.GetComponentInChildren<TextMeshProUGUI>().SetText(profiles[2].name);
        } 
        
        if (profiles.Count >= 4)
        {
            profile4Button.GetComponentInChildren<TextMeshProUGUI>().SetText(profiles[3].name);
        }
    }

    private void ReturnToMainMenu()
    {
        profilesSelectionPanel.SetActive(false);
        mainMenu.Show();
    }
}