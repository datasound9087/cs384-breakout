using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Script to control overal game flow
*/
public class GameManager : MonoBehaviour
{
    // Game settings
    public GameSettings gameSettings;
    public event Action OnGameOver; 
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnRestart;
    public event Action OnBallDeath;

    private ScoreManager scoreManager;
    private LevelManager levelManager;
    private AchievementManager achievementManager;
    private BallLauncher ballLauncher;
    private Ball ball;
    private Paddle paddle;
    private int lives;
    private bool gamePaused = false;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        achievementManager = FindObjectOfType<AchievementManager>();
        ballLauncher = FindObjectOfType<BallLauncher>();
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();

        lives = gameSettings.startingLives;

        // Subscribe various components to game events
        OnGameOver += achievementManager.Save;
        OnGameOver += scoreManager.Save;

        OnRestart += paddle.Reset;
        OnRestart += ball.Reset;
        OnRestart += levelManager.Restart;
        OnRestart += scoreManager.Reset;
        OnRestart += ballLauncher.Reset;

        OnBallDeath += paddle.Reset;
        OnBallDeath += ball.Reset;
        OnBallDeath += ballLauncher.Reset;

        levelManager.OnLevelComplete += this.LevelComplete;

        // Make sure that time scale is 1.0 (unpaused) otherwise game will not run
        UnFreezeTime();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            handlePause();
        }
    }

    private void FixedUpdate()
    {
        // Is game over
        if (lives == 0)
        {
            FreezeTime();
            OnGameOver();
        }

        // Ball is dead, reduce lives and reset
        if (ball.Dead)
        {
            lives--;
            OnBallDeath();
        }
    }

    private void handlePause()
    {
        // toggle game pause
        if (!gamePaused)
        {
            Pause();
        } else
        {
            Resume();
        }
    }

    public void Resume()
    {
        UnFreezeTime();
        gamePaused = false;
        OnResume();
    }

    public void Pause()
    {
        FreezeTime();
        gamePaused = true;
        OnPause();
    }
    
    public void Restart()
    {
        // Reset lives
        lives = gameSettings.startingLives;
        OnRestart();

        // If level was incomplete reset non persistent achievemnts
        if (!levelManager.LevelComplete())
        {
            achievementManager.Save();
            achievementManager.Reset();
        }
        
        // Resume game, can play again
        Resume();
    }

    public void NextLevel()
    {
        // Generate/load next level
        levelManager.NextLevel();
        Restart();
    }

    public int GetLives()
    {
        return lives;
    }

    // Pause game updates
    private void FreezeTime()
    {
        Time.timeScale = 0.0f;
    }

    // Reset time scale
    private void UnFreezeTime()
    {
        Time.timeScale = 1.0f;
    }

    public void LevelComplete(string name)
    {
        FreezeTime();
    }
}
