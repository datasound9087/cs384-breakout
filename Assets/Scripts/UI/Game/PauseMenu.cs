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
    }

    void Update()
    {
        if (!gameManager.GameOver() && gameManager.GetLives() > 0 && gameManager.GamePaused())
        {
            pausePanel.SetActive(true);
        } else
        {
            pausePanel.SetActive(false);
        }
    }
    public void OnResumeButtonClicked()
    {
        gameManager.Resume();
    }

    public void OnRestartButtonClicked()
    {
        gameManager.Restart();
        gameManager.Resume();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        gameManager.Resume(); // So that when relaunched game is unpaused
        menuSceneAnimator.TransitionToScene("MainMenu");
    }
}
