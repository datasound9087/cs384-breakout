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
    public SoundManager soundManager;

    private GameManager gameManager;
    private MenuSceneAnimator menuSceneAnimator;
    private const string LEVEL_TEXT = "Level: ";
    private const string SCORE_TEXT = "Score: ";
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();

        gameManager.OnGameOver += this.Show;
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    private void updateText()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        gameOverLevelText.GetComponent<TextMeshProUGUI>().text = LEVEL_TEXT + levelManager.GetLevelName();
        gameOverScoreText.GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + scoreManager.GetScore();
    }

    public void Show()
    {
        gameOverPanel.SetActive(true);
        updateText();
    }
}
