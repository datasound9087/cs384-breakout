using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    private float velocity = 0.0f;

    private Rigidbody2D paddleBody;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        paddleBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        float newX = transform.position.x + (velocity * movementSpeed) * Time.fixedDeltaTime;
        transform.position = new Vector3(newX, transform.position.y, 0);
    }
}
