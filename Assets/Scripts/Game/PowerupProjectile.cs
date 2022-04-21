using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for a powerup projectile.
*/
public class PowerupProjectile : MonoBehaviour
{
    // Name of powerup
    public string PowerupName { get; set; }
    private PowerupManager powerupManager;
    private Collider2D powerupCollider;
    private Rigidbody2D body;
    private Vector2 fallingVelocity;

    private void Awake()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
        powerupCollider = GetComponent<Collider2D>();

        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Make sure always falling at a constant speed
        body.velocity = fallingVelocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Do not collide with bricks, balls or other projectiles of this type
        // Doesn't always work for some reason :(
        if (ShouldIgnoreCollision(col))
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), powerupCollider);
            return;
        }

        if (col.gameObject.tag == "Paddle")
        {
            // Powerup collected, activate it
            powerupManager.AddPowerup(PowerupName);
            Destroy(this.gameObject);
        } else if (col.gameObject.tag == "DeathArea")
        {
            Destroy(this.gameObject);
        }
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