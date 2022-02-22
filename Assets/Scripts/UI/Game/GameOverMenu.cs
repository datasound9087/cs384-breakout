using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameOverLevelText;
    public GameObject gameOverScoreText;
    public GameObject circleFade;
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private LevelManager levelManager;
    private MenuSceneAnimator menuSceneAnimator;
    private const string LEVEL_TEXT = "Level: ";
    private const string SCORE_TEXT = "Score: ";
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        gameManager.Resume(); // So that when relaunched game is unpaused
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetLives() == 0)
        {
            gameOverPanel.SetActive(true);
            updateText();
        }
    }

    private void updateText()
    {
        gameOverLevelText.GetComponent<TextMeshProUGUI>().SetText(LEVEL_TEXT + levelManager.GetLevelName());
        gameOverScoreText.GetComponent<TextMeshProUGUI>().SetText(SCORE_TEXT + scoreManager.GetScore());
    }
}
