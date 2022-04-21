using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileCreationPanel : MonoBehaviour 
{
    public GameObject profileCreationPanel;
    public GameObject profileCreationInputField;
    public SoundManager soundManager;
    private MainMenu mainMenu;
    private Profile editedProfile = null;

    void Awake()
    {
        mainMenu = GetComponent<MainMenu>();
    }

    public void OnProfileCreationEnded()
    {
        string nameText = profileCreationInputField.GetComponent<TMP_InputField>().text;
        if (string.IsNullOrEmpty(nameText) || string.IsNullOrWhiteSpace(nameText))
        {
            return;
        }

        Profile profile = null;
        if (EditingProfile())
        {
            profile = editedProfile;
        } else
        {
            profile = new Profile();
            profile.endlessHighScore = 0;
            profile.levelsHighScore = 0;
            ProfileManager.Instance.AddProfile(profile);
        }

        profile.name = nameText;
        ProfileManager.Instance.SaveProfiles();
        ProfileManager.Instance.SetActiveProfile(profile);

        soundManager.PlaySound("MenuClick");
        ReturnToMainMenu();
    }

    public void Show()
    {
        profileCreationPanel.SetActive(true);
    }

    public void SetProfileData(Profile profile)
    {
        editedProfile = profile;
        profileCreationInputField.GetComponent<TMP_InputField>().text = profile.name;
    }

    private void ReturnToMainMenu()
    {
        profileCreationPanel.SetActive(false);
        mainMenu.Show();
    }

    private bool EditingProfile()
    {
        return editedProfile != null;
    }
}