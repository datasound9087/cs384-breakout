using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    public BrickColours brickColours;
    private bool hasPowerup = false;
    private bool unbreakable = false;
    private int durability = DurabilityConstants.Durability1;
    private PowerupInfo powerupInfo;

    public event Action OnHit;
    public event Action OnBreak;

    private PowerupSpawner powerupSpawner;
    private SpriteRenderer brickSprite;

    void Awake()
    {
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        brickSprite = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (!unbreakable)
            {
                OnHit();
                UpdateDurability();

                if (ShouldSpawnPowerup())
                {
                    powerupSpawner.SpawnPowerupProjectileFromBrick(this, powerupInfo);
                }

                if (durability == DurabilityConstants.Broken)
                {
                    OnBreak();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void setDurability(int durability)
    {
        this.durability = durability;
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
        this.powerupInfo = powerupInfo;
        hasPowerup = true;
    }
}
