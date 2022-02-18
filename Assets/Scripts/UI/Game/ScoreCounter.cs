using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private readonly string LIVES_TEXT = "Score: ";
    private ScoreManager scoreManager;
    
    // Cache score so don't update UI every frame - text rendering is slow
    private int cachedScore;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        cachedScore = scoreManager.GetScore();
        GetComponent<TextMeshProUGUI>().SetText(LIVES_TEXT + cachedScore);
    }

    void Update()
    {
        if (scoreManager.GetScore() != cachedScore)
        {
            cachedScore = scoreManager.GetScore();
            GetComponent<TextMeshProUGUI>().SetText(LIVES_TEXT + cachedScore);
        }
    }
}
