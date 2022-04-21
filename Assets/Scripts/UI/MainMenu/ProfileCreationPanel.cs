using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Profile Creation UI handler.
*/
public class ProfileCreationPanel : MonoBehaviour 
{
    public GameObject profileCreationPanel;
    public GameObject profileCreationInputField;
    public SoundManager soundManager;

    private MainMenu mainMenu;

    // The profile whose data is currently being edited
    private Profile editedProfile = null;

    private void Awake()
    {
        mainMenu = GetComponent<MainMenu>();
    }

    // Callback for when either ok button or enter is pressed in the input field
    public void OnProfileCreationEnded()
    {
        // Get text and check if valid, if not ignore
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
            // Create and initialise a new Profile as none exists
            profile = new Profile();
            profile.endlessHighScore = 0;
            profile.levelsHighScore = 0;
            ProfileManager.Instance.AddProfile(profile);
        }

        profile.name = nameText;
        ProfileManager.Instance.SaveProfiles();

        // This profile will now be used for all subsequent stuffs
        ProfileManager.Instance.SetActiveProfile(profile);

        soundManager.PlaySound("MenuClick");
        ReturnToMainMenu();
    }

    public void Show()
    {
        profileCreationPanel.SetActive(true);
    }
    
    // Set propfile to edit
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