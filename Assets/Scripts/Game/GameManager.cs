using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private BrickManager brickManager;
    private Ball ball;
    private Paddle paddle;
    private bool gameBegun = false;

    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        brickManager = FindObjectOfType<BrickManager>();
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
    }

    void FixedUpdate()
    {
        if (ball.dead)
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
}
