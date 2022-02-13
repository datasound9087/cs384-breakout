using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GamePaused())
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
        SceneManager.LoadSceneAsync("MainMenu");
        gameManager.Resume(); // So that when relaunched game is unpaused
    }
}
