using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float movementSpeed = 200.0f;
    public float startingAngleRange = 45.0f;

    public event Action OnBounce;

    private Paddle paddle;
    private Rigidbody2D ballBody;
    private Rigidbody2D paddleBody;
    private Vector3 startPosition;
    private bool stuck = true;
    
    public bool Dead { get; private set; } = false;

    // Start is called before the first frame update
    void Awake()
    {
        paddle = FindObjectOfType<Paddle>();
        ballBody = GetComponent<Rigidbody2D>();
        paddleBody = paddle.GetComponent<Rigidbody2D>();

        startPosition = ballBody.position;
    }

    void FixedUpdate()
    {
        // Do not update ball if should not move
        if (stuck)
        {
            followPaddlePosition();
        } else
        {
            if (ballBody.velocity.y == 0.0f || ballBody.velocity.x == 0.0f)
            {
                // TODO
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeathArea")
        {
            Dead = true;
        }

        OnBounce();
    }

    private void followPaddlePosition()
    // Quick way to get ball to follow paddle. Has an amusing wobble to it :)
    {
        ballBody.velocity = paddleBody.velocity;
    }

    public void Launch()
    {   
        // If paddle moving on launch - narrow random direction to moving side to feel more predictable
        // Moving Left
        float startRange =  paddle.InputVelocity > 0 ? 0 : -startingAngleRange;
        float endRange = paddle.InputVelocity < 0 ? 0 : startingAngleRange;

        float randAngle = UnityEngine.Random.Range(startRange, endRange);
        Quaternion rotation = Quaternion.Euler(0, 0, randAngle);
        ballBody.velocity = rotation * Vector2.down * movementSpeed;
        stuck = false;
    }

    public void Reset()
    {
        ballBody.velocity = Vector2.zero;
        ballBody.position = startPosition;
        stuck = true;
        Dead = false;
    }
}
