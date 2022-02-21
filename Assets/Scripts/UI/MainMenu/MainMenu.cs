using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject pressToStartText;
    public GameObject OptionsPanel;
    public GameObject ProfilesSelectionPanel;
    public GameObject profile1Button;
    public GameObject profile2Button;
    public GameObject profile3Button;
    public GameObject profile4Button;
    public GameObject ProfileCreationPanel;
    public GameObject profileCreationInputField;

    void Awake()
    {
        PopulateProfiles();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressToStartText.SetActive(false);
            ProfilesSelectionPanel.SetActive(true);
        }
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadSceneAsync("PlayMenu");
    }

    public void OnOptionsButtonClicked()
    {
        SceneManager.LoadSceneAsync("OptionsMenu");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnProfileButtonClicked(int profileIndex)
    {
        if (!ProfileExists(profileIndex))
        {
            ShowProfileCreationPanel();
        } else
        {
            ProfileManager.Instance.SetActiveProfile(profileIndex - 1);
            ProfilesSelectionPanel.SetActive(false);
            ShowOptionsPanel();
        }
    }

    public void OnProfileCreationEnded()
    {
        Profile profile = new Profile();
        profile.name = profileCreationInputField.GetComponent<TMP_InputField>().text;
        profile.endlessHighScore = 0;
        profile.levelsHighScore = 0;
        if (string.IsNullOrEmpty(profile.name) || string.IsNullOrWhiteSpace(profile.name))
        {
            return;
        }
        ProfileManager.Instance.AddActiveProfile(profile);
        ProfileManager.Instance.SaveProfiles();

        ProfileCreationPanel.SetActive(false);
        ShowOptionsPanel();
    }

    private void ShowProfileCreationPanel()
    {
        ProfilesSelectionPanel.SetActive(false);
        ProfileCreationPanel.SetActive(true);
    }

    private void ShowOptionsPanel()
    {
        OptionsPanel.SetActive(true);
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
}
