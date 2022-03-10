using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public float movementSpeed = 200.0f;

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

        // If the ball has collided on a corner it is possible that it's velocity has changed
        // Therefore reset it to it's intended speed
        ballBody.velocity = movementSpeed * ballBody.velocity.normalized;
    }

    public void SetDirection(Vector3 direction)
    {
        ballBody.velocity = direction * movementSpeed;
    }

    public void Reset()
    {
        ballBody.velocity = Vector2.zero;
        ballBody.position = startPosition;
        Dead = false;
    }
}
