using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallLauncher : MonoBehaviour
{
    public float launchAngleRange;

    private Paddle paddle;
    private Rigidbody2D paddleBody;
    private Ball ball;
    private Rigidbody2D ballBody;
    private bool launched = false;

    void Awake()
    {
        ball = GetComponent<Ball>();
        ballBody = ball.GetComponent<Rigidbody2D>();
        paddle = FindObjectOfType<Paddle>();
        paddleBody = paddle.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!launched && Input.GetKeyDown("space"))
        {
            Launch();
        }
    }

    void FixedUpdate()
    {
        if (!launched)
        {
            FollowPaddlePosition();
        }
    }

    public void Launch()
    {
        // If paddle moving on launch - narrow random direction to moving side to feel more predictable
        // Moving Left
        float startRange =  paddle.InputVelocity > 0 ? 0 : -launchAngleRange;
        float endRange = paddle.InputVelocity < 0 ? 0 : launchAngleRange;

        float angle = UnityEngine.Random.Range(startRange, endRange);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        ball.SetSpeed(rotation * Vector3.down);
        launched = true;
    }

    private void FollowPaddlePosition()
    {
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