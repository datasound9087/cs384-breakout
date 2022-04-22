using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Script to manage game scores.
*/
public class ScoreManager : MonoBehaviour
{
    public GameSettings gameSettings;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;

    // Users current score data
    private int score;
    private int highScore;

    private void Awake()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnLevelComplete += this.OnLevelComplete;

        // Load relevant score dta for game mode
        if (gameSettings.endlessMode)
        {
            highScore = ProfileManager.Instance.GetActiveProfile().endlessHighScore;
        } else
        {
            highScore = ProfileManager.Instance.GetActiveProfile().levelsHighScore;
        }
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        score = 0;
        OnScoreChanged(score);
    }

    public void IncrementScore()
    {
        // Update high score if applicable
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

    public void OnLevelComplete(string level)
    {
        Save();
    }

    public void Save()
    {
        // If new highscore data to be saved, save it
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
