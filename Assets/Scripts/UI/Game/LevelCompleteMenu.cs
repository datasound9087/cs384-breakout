using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public GameObject levelCompleteScoreText;
    public GameObject levelCompleteLevelText;
    public GameObject circleFade;
    public Button continueButton;
    public GameSettings gameSettings;
    public SoundManager soundManager;
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

        levelManager.OnLevelComplete += this.Show;
        gameManager.OnResume += this.Hide;
    }

    public void OnContinueButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameManager.NextLevel();
    }

    public void onRestartButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameManager.Restart();
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    private void UpdateText()
    {
        levelCompleteScoreText.GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + scoreManager.GetScore();
        levelCompleteLevelText.GetComponent<TextMeshProUGUI>().text = LEVEL_TEXT + levelManager.GetLevelName();
    }

    public void Show(string name)
    {
        if (!gameSettings.endlessMode && levelManager.EndOfLevels())
        {
            continueButton.interactable = false;
        }

        UpdateText();
        levelCompletePanel.SetActive(true);
    }

    public void Hide()
    {
        levelCompletePanel.SetActive(false);
    }
}
