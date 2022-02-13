using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    private int score;
    private int highScore;
    void Start()
    {
        highScore = 0;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        score = 0;
    }

    public void IncrementScore()
    {
        score += 1;
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}