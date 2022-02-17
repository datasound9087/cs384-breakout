using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game settings
    public GameSettings gameSettings;
    private BrickManager brickManager;
    private ScoreManager scoreManager;
    private Ball ball;
    private Paddle paddle;
    private bool gameBegun = false;

    private static readonly int MAX_LIVES = 3;
    private int lives = MAX_LIVES;
    private bool gamePaused = false;
    private bool gameFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        brickManager = FindObjectOfType<BrickManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
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

    public bool GameFinished()
    {
        return gameFinished;
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
        lives = MAX_LIVES;
        paddle.Reset();
        ball.Reset();
        brickManager.Reset();

        gameBegun = false;
        scoreManager.Reset();
    }
}
