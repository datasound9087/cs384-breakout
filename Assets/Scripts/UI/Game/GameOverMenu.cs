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
    private GameManager gameManager;
    private ScoreManager scoreManager;
    private LevelManager levelManager;
    private readonly string LEVEL_TEXT = "Level: ";
    private readonly string SCORE_TEXT = "Score: ";
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        gameManager.Resume(); // So that when relaunched game is unpaused
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameOver() && gameManager.GetLives() > 0)
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
