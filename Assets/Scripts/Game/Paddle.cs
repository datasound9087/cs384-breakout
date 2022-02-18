using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movementSpeed = 400.0f;
    private float inputVelocity = 0.0f;
    public float InputVelocity { get { return inputVelocity; }}
    private Rigidbody2D paddleBody;

    private Vector3 startPosition;

    void Awake()
    {
        paddleBody = GetComponent<Rigidbody2D>();
        startPosition = paddleBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputVelocity = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Updating position directly messes with box2d collion detection, creating overlaps
        // therefore, update velocities instead to control movement
        float newX = inputVelocity * movementSpeed * Time.fixedDeltaTime;
        paddleBody.velocity = new Vector2(newX, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            inputVelocity *= 0.0f;
        }
    }

    public void Reset()
    {
        paddleBody.position = startPosition;
    }
}
