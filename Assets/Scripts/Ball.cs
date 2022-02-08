using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool stuck { get; set; } = true;

    private Rigidbody2D ballBody;
    private CircleCollider2D ballCollider;

    private Paddle paddle;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<CircleCollider2D>();

        paddle = FindObjectOfType<Paddle>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        
        /*if (gameManager.GameBegun)
        {
            stuck = false;
        }
        else
        {
            followPaddlePosition();
        }*/

        // Do not update ball if should not move
        if (stuck)
        {
            return;
        }
    }

    private void followPaddlePosition()
    {
        transform.position = paddle.Position;
    }
}
