using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public BrickColours brickColours;
    private bool hasPowerup = false;
    private bool unbreakable = false;
    private int durability = DurabilityConstants.Durability1;
    private PowerupInfo powerupInfo;

    private LevelManager levelManager;
    private PowerupSpawner powerupSpawner;
    private ScoreManager scoreManager;
    private SpriteRenderer brickSprite;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        brickSprite = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            scoreManager.IncrementScore();
            UpdateDurability();

            if (ShouldSpawnPowerup())
            {
                powerupSpawner.SpawnPowerupProjectileFromBrick(this, powerupInfo);
            }

            if (!unbreakable && durability == DurabilityConstants.Broken)
            {
                levelManager.BrickDestroyed();
                Destroy(this.gameObject);
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
