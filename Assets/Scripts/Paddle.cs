using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movementSpeed = 400.0f;
    private float inputVelocity = 0.0f;
    private Rigidbody2D paddleBody;

    // Start is called before the first frame update
    void Start()
    {
        paddleBody = GetComponent<Rigidbody2D>();
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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            inputVelocity *= 0.0f;
        }
    }
}
