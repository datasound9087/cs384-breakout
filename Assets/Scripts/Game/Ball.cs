using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float movementSpeed = 200.0f;
    public Vector2 minVelocities;
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

        // If the ball is moving too horizontally correct it
        Vector2 fixedVelocity = ballBody.velocity;
        if (ballBody.velocity.y > 0.0f && ballBody.velocity.y < minVelocities.y)
        {
            fixedVelocity.y = minVelocities.y;
        }
        else if (ballBody.velocity.y < 0.0f & ballBody.velocity.y > -minVelocities.y)
        {
            fixedVelocity.y = -minVelocities.y;
        }
        
        if (ballBody.velocity.x > 0.0f && ballBody.velocity.x < minVelocities.x)
        {
            fixedVelocity.x = minVelocities.x;
        }
        else if (ballBody.velocity.x < 0.0f & ballBody.velocity.x > -minVelocities.x)
        {
            fixedVelocity.x = -minVelocities.x;
        }

        SetSpeed(fixedVelocity);

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
