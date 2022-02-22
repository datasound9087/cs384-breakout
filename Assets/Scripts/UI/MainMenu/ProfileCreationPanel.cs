using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileCreationPanel : MonoBehaviour 
{
    public GameObject profileCreationPanel;
    public GameObject profileCreationInputField;
    private MainMenu mainMenu;

    void Awake()
    {
        mainMenu = GetComponent<MainMenu>();
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

        profileCreationPanel.SetActive(false);
        ReturnToMainMenu();
    }

    public void Show()
    {
        profileCreationPanel.SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        profileCreationPanel.SetActive(false);
        mainMenu.Show();
    }
}