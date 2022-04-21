using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Script to handle ball launching from game start.
*/
public class BallLauncher : MonoBehaviour
{
    // Angle in degrees either side of vertical that ball can launch into
    public float launchAngleRange;

    private Paddle paddle;
    private Rigidbody2D paddleBody;
    private Ball ball;
    private Rigidbody2D ballBody;

    // Has the ball launched
    private bool launched = false;

    private void Awake()
    {
        ball = GetComponent<Ball>();
        ballBody = ball.GetComponent<Rigidbody2D>();
        paddle = FindObjectOfType<Paddle>();
        paddleBody = paddle.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Launch ball
        if (!launched && Input.GetKeyDown("space"))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        // If ball hasn't launch follow paddle
        if (!launched)
        {
            FollowPaddlePosition();
        }
    }

    public void Launch()
    {
        // If paddle moving on launch - narrow random direction to moving side to feel more predictable
        float startRange =  paddle.InputVelocity > 0 ? 0 : -launchAngleRange;
        float endRange = paddle.InputVelocity < 0 ? 0 : launchAngleRange;

        // Calculate ball launch direction and, well, launch :)
        float angle = UnityEngine.Random.Range(startRange, endRange);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        ball.SetSpeed(rotation * Vector3.down);
        launched = true;
    }

    private void FollowPaddlePosition()
    {
        // Deliberate so ball kinda jiggles around on paddle - amusing
        ballBody.velocity = paddleBody.velocity;
    }

    public void Reset()
    {
        launched = false;
    }

    public bool Launched()
    {
        return launched;
    }
}