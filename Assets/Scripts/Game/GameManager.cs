using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        achievementManager = FindObjectOfType<AchievementManager>();
        ballLauncher = FindObjectOfType<BallLauncher>();
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();

        lives = gameSettings.startingLives;

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

        // Make sure that time scale is 1.0 (unpaused)
        UnFreezeTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            handlePause();
        }
    }

    void FixedUpdate()
    {
        if (ballLauncher.Launched())
        {
            if (lives == 0)
            {
                FreezeTime();
                OnGameOver();
            }

            if (ball.Dead)
            {
                lives--;
                OnBallDeath();
            }
        }
    }

    private void handlePause()
    {
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
        Debug.Log("s");
        OnRestart();
        lives = gameSettings.startingLives;

        // If level was incomplete reset non persistent achievemnts
        if (!levelManager.LevelComplete())
        {
            achievementManager.Save();
            achievementManager.Reset();
        }

        Resume();
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
        Restart();
    }

    public int GetLives()
    {
        return lives;
    }

    private void FreezeTime()
    {
        Time.timeScale = 0.0f;
    }

    private void UnFreezeTime()
    {
        Time.timeScale = 1.0f;
    }

    public void LevelComplete(string name)
    {
        FreezeTime();
    }
}
