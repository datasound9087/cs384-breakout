using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float movementSpeed = 200.0f;
    public Vector2 minVelocities;
    public event Action OnBounce;
    private Rigidbody2D ballBody;
    private Vector3 startPosition;
    
    public bool Dead { get; private set; } = false;

    // Start is called before the first frame update
    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        startPosition = ballBody.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeathArea")
        {
            Dead = true;
        }

        if (OnBounce != null)
        {
            OnBounce();
        }

        // If the ball is moving too horizontally correct it
        if (ballBody.velocity.y > 0.0f && ballBody.velocity.y < minVelocities.y)
        {
            SetSpeed(new Vector2(ballBody.velocity.x, minVelocities.y));
        }
        else if (ballBody.velocity.y < 0.0f & ballBody.velocity.y > -minVelocities.y)
        {
            SetSpeed(new Vector2(ballBody.velocity.x, -minVelocities.y));
        }
        else
        {
            SetSpeed(ballBody.velocity);
        }
    }

    public void SetSpeed(Vector2 direction)
    {
        ballBody.velocity = direction.normalized * movementSpeed;
    }

    public void Reset()
    {
        ballBody.velocity = Vector2.zero;
        ballBody.position = startPosition;
        Dead = false;
    }
}
