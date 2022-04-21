using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Level Complete UI handler.
*/
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

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();

        // Show when a level is complete and hide when game starts running
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

    // Display relevant score and level info
    private void UpdateText()
    {
        levelCompleteScoreText.GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + scoreManager.GetScore();
        levelCompleteLevelText.GetComponent<TextMeshProUGUI>().text = LEVEL_TEXT + levelManager.GetLevelName();
    }

    public void Show(string name)
    {
        // If at end of defined level chain disable continue button as there's no level to play next
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
