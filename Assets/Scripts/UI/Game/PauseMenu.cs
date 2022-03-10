using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject circleFade;
    private GameManager gameManager;
    private MenuSceneAnimator menuSceneAnimator;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();

        gameManager.OnPause += this.Show;
        gameManager.OnResume += this.Hide;
    }

    public void OnResumeButtonClicked()
    {
        gameManager.Resume();
    }

    public void OnRestartButtonClicked()
    {
        gameManager.Resume();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    public void Show()
    {
        pausePanel.SetActive(true);
    }

    public void Hide()
    {
        pausePanel.SetActive(false);
    }
}
