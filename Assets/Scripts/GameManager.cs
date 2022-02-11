using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private BrickManager brickManager;
    private Ball ball;
    private Paddle paddle;
    private bool gameBegun = false;

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

    private void FixedUpdate()
    {
        if (ball.dead)
        {
            paddle.Reset();
            ball.Reset();
            gameBegun = false;
        }
    }
}
