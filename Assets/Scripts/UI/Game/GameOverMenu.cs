using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Game Over UI Handler.
*/
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
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuSceneAnimator = circleFade.GetComponent<MenuSceneAnimator>();

        // Show when a Game over event is fired
        gameManager.OnGameOver += this.Show;
    }

    public void OnQuitToMainMenuButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("MainMenu");
    }

    // Display relevant score and level info
    private void updateText()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        gameOverLevelText.GetComponent<TextMeshProUGUI>().text = LEVEL_TEXT + levelManager.GetLevelName();
        gameOverScoreText.GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + scoreManager.GetScore();
    }

    public void Show()
    {   
        // Show the game over UI
        gameOverPanel.SetActive(true);
        updateText();
    }
}
