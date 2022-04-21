using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Score UI handler.
*/
public class ScoreCounter : MonoBehaviour
{
    private const string SCORE_TEXT = "Score: ";
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.OnScoreChanged += this.OnScoreChanged;
    }

    private void Start()
    {
        OnScoreChanged(scoreManager.GetScore());
    }

    // Display score
    public void OnScoreChanged(int score)
    {
        GetComponent<TextMeshProUGUI>().text = SCORE_TEXT + score;
    }
}
