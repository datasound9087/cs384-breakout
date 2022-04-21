using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Game Ball.
*/
public class Ball : MonoBehaviour
{
    //Speed of ball
    public float movementSpeed;

    // Min velocities in each direction. This is to prevent the ball moving too horizontally/vertically (as that's boring)
    // Also to prevent the ball moving completely horizontally/vertically and getting stuck
    public Vector2 minVelocities;

    private Rigidbody2D ballBody;
    private Vector3 startPosition;
    
    // Has the ball gone off the level
    public bool Dead { get; private set; } = false;

    // Start is called before the first frame update
    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        startPosition = ballBody.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Ball has gone out of the level
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
        
        // If the ball is moving too vertically correct it
        if (ballBody.velocity.x > 0.0f && ballBody.velocity.x < minVelocities.x)
        {
            fixedVelocity.x = minVelocities.x;
        }
        else if (ballBody.velocity.x < 0.0f & ballBody.velocity.x > -minVelocities.x)
        {
            fixedVelocity.x = -minVelocities.x;
        }

        // Set after collision as Unity's physics may have changed its speed
        SetSpeed(fixedVelocity);
    }

    public void SetSpeed(Vector2 direction)
    {
        // Make sure ball is always travelling at a constant velocity in a direction
        ballBody.velocity = direction.normalized * movementSpeed;
    }

    public void Reset()
    {
        // Set ball back to its start position
        ballBody.velocity = Vector2.zero;
        ballBody.position = startPosition;
        Dead = false;
    }
}
