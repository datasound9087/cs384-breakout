using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Game settings
    public GameSettings gameSettings;

    public event Action OnGameOver; 

    private ScoreManager scoreManager;
    private LevelManager levelManager;
    private Ball ball;
    private Paddle paddle;
    private bool gameBegun = false;
    private int lives;
    private bool gamePaused = false;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindObjectOfType<LevelManager>();
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();

        lives = gameSettings.startingLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameBegun && Input.GetKeyDown("space"))
        {
            ball.Launch();
            gameBegun = true;
        }
        else
        {
            if (Input.GetKeyDown("escape"))
            {
                handlePause();
            }
        }
    }

    void FixedUpdate()
    {
        if (lives == 0)
        {
            OnGameOver();
            Pause();
        }
        
        if (lives == 0 || GameOver())
        {
            OnGameOver();
            scoreManager.SaveScore();
            Pause();
        }

        if (ball.Dead)
        {
            lives--;
            paddle.Reset();
            ball.Reset();
            gameBegun = false;
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public bool GamePaused()
    {
        return gamePaused;
    }

    public bool GameOver()
    {
        return levelManager.LevelFinished();
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
        Time.timeScale = 1.0f;
        gamePaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        gamePaused = true;
    }
    
    public void Restart()
    {
        lives = gameSettings.startingLives;
        paddle.Reset();
        ball.Reset();
        levelManager.ResetLevel();

        gameBegun = false;
        scoreManager.Reset();
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
        Restart();
    }
}
