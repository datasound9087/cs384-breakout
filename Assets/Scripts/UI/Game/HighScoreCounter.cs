using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreCounter : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "High Score: ";
    private ScoreManager scoreManager;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnHighScoreChanged += this.HighScoreChanged;
    }

    void Start()
    {
        HighScoreChanged(scoreManager.GetHighScore());
    }

    public void HighScoreChanged(int highScore)
    {
        GetComponent<TextMeshProUGUI>().text = HIGH_SCORE_TEXT + highScore;
    }
}
