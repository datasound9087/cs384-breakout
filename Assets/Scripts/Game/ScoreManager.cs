using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameSettings gameSettings;
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
