using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public GameObject levelCompleteScoreText;
    public GameObject levelCompleteLevelText;
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

    void Update()
    {
        if (gameManager.GameOver())
        {
            levelCompletePanel.SetActive(true);
            UpdateText();
        }
    }

    public void OnContinueButtonClicked()
    {
        levelManager.NextLevel();
        gameManager.NextLevel();
        gameManager.Resume();
        levelCompletePanel.SetActive(false);
    }

    public void onRestartButtonClicked()
    {
        gameManager.Restart();
        gameManager.Resume();
        levelCompletePanel.SetActive(false);
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        gameManager.Resume();
    }

    private void UpdateText()
    {
        levelCompleteScoreText.GetComponent<TextMeshProUGUI>().SetText(SCORE_TEXT + scoreManager.GetScore());
        levelCompleteLevelText.GetComponent<TextMeshProUGUI>().SetText(LEVEL_TEXT + levelManager.GetLevelName());
    }
}
