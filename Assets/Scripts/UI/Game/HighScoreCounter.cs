using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * High Score UI Handler.
*/
public class HighScoreCounter : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "High Score: ";
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        // Update when the high score changes
        scoreManager.OnHighScoreChanged += this.HighScoreChanged;
    }

    private void Start()
    {
        // Read current high score
        HighScoreChanged(scoreManager.GetHighScore());
    }

    // Display high score
    public void HighScoreChanged(int highScore)
    {
        GetComponent<TextMeshProUGUI>().text = HIGH_SCORE_TEXT + highScore;
    }
}
