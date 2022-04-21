using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Main Menu UI Handler.
*/
public class MainMenu : MonoBehaviour
{
    public GameObject pressToStartText;
    public GameObject optionsPanel;
    public SoundManager soundManager;

    private ProfilePanel profilePanel;
    private MenuSceneAnimator menuSceneAnimator;

    // Has the game started (has the user pressed space once)
    private bool started = false;

    private void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
        profilePanel = GetComponent<ProfilePanel>();
    }

    void Update()
    {
        if (!started && Input.GetKeyDown("space"))
        {
            started = true;
            soundManager.PlaySound("MenuClick");
            pressToStartText.SetActive(false);
            profilePanel.LoadProfiles();
            profilePanel.Show();
        }
    }

    public void OnPlayButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("PlayMenu");
    }

    public void OnAchievementsButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("AchievementsMenu");
    }

    public void OnOptionsButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("OptionsMenu");
    }

    public void OnQuitButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        Application.Quit();
    }

    public void Show()
    {
        optionsPanel.SetActive(true);
    }
}
