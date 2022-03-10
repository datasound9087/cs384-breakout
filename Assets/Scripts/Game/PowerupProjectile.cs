using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupProjectile : MonoBehaviour
{
    public string PowerupName { get; set; }
    private PowerupManager powerupManager;
    private Collider2D powerupCollider;
    private Rigidbody2D body;
    private Vector2 fallingVelocity;
    void Awake()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        powerupCollider = GetComponent<Collider2D>();

        body = GetComponent<Rigidbody2D>();

        //body.velocity = Vector2.down * 2.0f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (ShouldIgnoreCollision(col))
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), powerupCollider);
            return;
        }

        if (col.gameObject.tag == "Paddle")
        {
            powerupManager.AddPowerup(PowerupName);
            Destroy(this.gameObject);
        } else if (col.gameObject.tag == "DeathArea")
        {
            Destroy(this.gameObject);
        }

        body.velocity = fallingVelocity;
    }

    private bool ShouldIgnoreCollision(Collision2D col)
    {
        return col.gameObject.tag == "Brick" || col.gameObject.tag == "Ball"
                || col.gameObject.tag == "PowerupProjectile";
    }

    public void SetFallingVelocity(Vector2 fallingSpeed)
    {
        this.fallingVelocity = fallingSpeed;
    }
}