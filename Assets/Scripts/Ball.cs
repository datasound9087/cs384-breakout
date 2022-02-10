using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool stuck { get; set; } = false;
    public float movementSpeed = 200.0f;
    public float startingAngleRange = 45.0f;
    public GameObject paddle;
    private Rigidbody2D ballBody;
    private Rigidbody2D paddleBody;

    // Start is called before the first frame update
    void Start()
    {
        ballBody = GetComponent<Rigidbody2D>();
        paddleBody = paddle.GetComponent<Rigidbody2D>();

        launchBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        // Do not update ball if should not move
        if (stuck)
        {
            followPaddlePosition();
        }
    }

    // Quick way to get ball to follow paddle. Has an amusing wobble to it :)
    private void followPaddlePosition()
    {
        ballBody.velocity = paddleBody.velocity;
    }

    private void launchBall()
    { 
        float randAngle = Random.Range(-startingAngleRange, startingAngleRange);
        Quaternion rotation = Quaternion.Euler(0, 0, randAngle);
        ballBody.velocity = rotation * Vector2.down * movementSpeed;
    }
}
