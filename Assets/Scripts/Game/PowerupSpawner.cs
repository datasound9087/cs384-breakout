using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to spawn powerups at a brick location.
*/
public class PowerupSpawner : MonoBehaviour
{
    // Powerup projectile prefab
    public GameObject powerupProjectile;

    // How fast should a powerup projectile fall
    public float fallingSpeed;

    public void SpawnPowerupProjectileFromBrick(Brick brick, PowerupInfo info)
    {
        // Spawn at same size and position of related brick
        Vector3 brickPosition = brick.GetComponent<Transform>().position;
        Vector3 powerupPosition = new Vector3(brickPosition.x, brickPosition.y, brickPosition.z);

        PowerupProjectile projectile = Instantiate(powerupProjectile, powerupPosition, Quaternion.identity, this.transform).GetComponent<PowerupProjectile>();
        projectile.transform.localScale = brick.transform.localScale;

        projectile.PowerupName = info.GetName();
        projectile.SetFallingVelocity(Vector2.down * fallingSpeed);
    }
}
