using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Pause Menu UI handler.
*/
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject circleFade;
    public SoundManager soundManager;

    private GameManager gameManager;
    private MenuSceneAnimator menuSceneAnimator;
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();

        // Show on pause and hide on resume
        gameManager.OnPause += this.Show;
        gameManager.OnResume += this.Hide;
    }

    public void OnResumeButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameManager.Resume();
    }

    public void OnRestartButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameManager.Restart();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
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
