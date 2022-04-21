using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Paddle script.
*/
public class Paddle : MonoBehaviour
{
    // Default ovement speed
    public float movementSpeed;

    // What direction am curtrently moving in
    public float InputVelocity { get { return inputVelocity; }}
    private float inputVelocity = 0.0f;

    private Rigidbody2D paddleBody;
    private float speed;
    private Vector3 startPosition;

    private void Awake()
    {
        paddleBody = GetComponent<Rigidbody2D>();
        startPosition = paddleBody.position;
        speed = movementSpeed;
    }

    private void Update()
    {
        inputVelocity = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // Updating position directly messes with box2d collion detection, creating overlaps
        // therefore, update velocities instead to control movement
        float newX = inputVelocity * speed * Time.fixedDeltaTime;
        paddleBody.velocity = new Vector2(newX, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Stop at border
        if (col.gameObject.tag == "Border")
        {
            inputVelocity = 0.0f;
        }
    }

    public void Reset()
    {
        // Go back to starting position with default speed
        paddleBody.position = startPosition;
        speed = movementSpeed;
    }

    public void IncreaseSpeed()
    {
        // Double speed if not already moving fast
        if (speed == movementSpeed)
        {
            speed *= 2.0f;
        }
    }

    public void DecreaseSpeed()
    {
        speed = movementSpeed;
    }
}
