using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Script attached to Brick objects.
*/
public class Brick : MonoBehaviour
{
    // Colours for each durability
    public BrickColours brickColours;
    public event Action OnHit;
    public event Action OnDurabilityChange;
    public event Action OnBreak;

    private bool hasPowerup = false;
    private bool unbreakable = false;

    // How many hits will it take to destroy this brick. Default is 1
    private int durability = DurabilityConstants.Durability1;

    // The assocaited powerup info, if applicable
    private PowerupInfo powerupInfo;

    private PowerupSpawner powerupSpawner;
    private SpriteRenderer brickSprite;

    private void Awake()
    {
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        brickSprite = GetComponent<SpriteRenderer>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        OnHit();
        if (col.gameObject.tag == "Ball")
        {
            if (!unbreakable)
            {
                OnDurabilityChange();
                UpdateDurability();

                // Spawn powerup if applicable
                if (ShouldSpawnPowerup())
                {
                    powerupSpawner.SpawnPowerupProjectileFromBrick(this, powerupInfo);
                }

                // Remove brick if has been broken
                if (durability == DurabilityConstants.Broken)
                {
                    OnBreak();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void SetDurability(int durability)
    {
        this.durability = durability;
        //Brick is unbreakable
        if (this.durability < DurabilityConstants.Broken)
        {
            unbreakable = true;
        }

        UpdateColour();
    }

    private void UpdateDurability()
    {
        if (!unbreakable)
        {
            durability--;
            UpdateColour();
        }
    }

    private bool ShouldSpawnPowerup()
    {
        bool canSpawn = false;
        if (hasPowerup)
        {
            // Has this brick met the conditions to be able to spawn a powerup
            bool durabilityMatch = durability == powerupInfo.GetDurability();
            canSpawn = powerupInfo.OnHit() ? true : durabilityMatch;
        }
        return canSpawn;
    }

    private void UpdateColour()
    {
        brickSprite.color = brickColours.GetColourForDurability(durability);
    }

    public void SetPowerupInfo(PowerupInfo powerupInfo)
    {
        // Brick now has a powerup
        this.powerupInfo = powerupInfo;
        hasPowerup = true;
    }
}
