using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public GameSettings gameSettings;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;

    private int score;
    private int highScore;

    void Awake()
    {
        if (gameSettings.endlessMode)
        {
            highScore = ProfileManager.Instance.GetActiveProfile().endlessHighScore;
        } else
        {
            highScore = ProfileManager.Instance.GetActiveProfile().levelsHighScore;
        }
        Reset();
    }

    public void Reset()
    {
        score = 0;
    }

    public void IncrementScore()
    {
        score += 1;
        OnScoreChanged(score);
        if (score > highScore)
        {
            highScore = score;
            OnHighScoreChanged(highScore);
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

    public void Save()
    {
        // If new highscore datat to be saved
        if (score == highScore)
        {
            if (gameSettings.endlessMode)
            {
                ProfileManager.Instance.GetActiveProfile().endlessHighScore = highScore;
            } else
            {
                ProfileManager.Instance.GetActiveProfile().levelsHighScore = highScore;
            }
            ProfileManager.Instance.SaveProfiles();
        }
    }
}
