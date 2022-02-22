using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject pressToStartText;
    public GameObject optionsPanel;
    private ProfilePanel profilePanel;
    private MenuSceneAnimator menuSceneAnimator;

    void Awake()
    {
        
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
        profilePanel = GetComponent<ProfilePanel>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressToStartText.SetActive(false);
            profilePanel.LoadProfiles();
            profilePanel.Show();
        }
    }

    public void OnPlayButtonClicked()
    {
        menuSceneAnimator.TransitionToScene("PlayMenu");
    }

    public void OnOptionsButtonClicked()
    {
        menuSceneAnimator.TransitionToScene("OptionsMenu");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void Show()
    {
        optionsPanel.SetActive(true);
    }
}
