using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreCounter : MonoBehaviour
{
    private readonly string LIVES_TEXT = "High Score: ";
    private ScoreManager scoreManager;
    
    // Cache high score so don't update UI every frame - text rendering is slow
    private int cachedHighScore;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        cachedHighScore = scoreManager.GetHighScore();
        GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + cachedHighScore;
    }

    void Update()
    {
        if (scoreManager.GetHighScore() != cachedHighScore)
        {
            cachedHighScore = scoreManager.GetHighScore();
            GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + cachedHighScore;
        }
    }
}
