using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private const string SCORE_TEXT = "Score: ";
    private ScoreManager scoreManager;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnScoreChanged += this.ScoreChanged;
    }

    void Start()
    {
        ScoreChanged(scoreManager.GetScore());
    }

    public void ScoreChanged(int score)
    {
        GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + score;
    }
}
