using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Endless mode brick generation.
*/
public class EndlessSpawning : IBrickSpawning
{
    private LevelManager levelManager;
    private PowerupManager powerupManager;

    // How likely are we to spawna a brick at each location (1 in 5)
    private const float BrickSpawnChance = 0.2f;

    // How likely is it that a spawned brick is unbreakable (1 in 10)
    private const float BrickUnbreakableChance = 0.1f;

    public EndlessSpawning(LevelManager levelManager, PowerupManager powerupManager)
    {
        this.levelManager = levelManager;
        this.powerupManager = powerupManager;
    }

    // Called on each brick position - should a brick be placed there
    public bool OnPlace(int x, int y)
    {
        // Soawn brick if allowed
        float val = Random.value;
        if  (val < BrickSpawnChance)
        {
            levelManager.NumBricksRemaining++;
            return true;
        }

        return false;
    }

    // Randomly initialise each bricks properties
    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        // Calculate durability
        if (Random.value < BrickUnbreakableChance)
        {
            brick.SetDurability(DurabilityConstants.Unbreakable);
        }
        else
        {
            brick.SetDurability(CalculateDurability(DurabilityConstants.Durability1));
        }

        // Generate powerup info
        float shouldSpawnPowerup = Random.value;
        if (shouldSpawnPowerup < 0.2f)
        {
            string chosenPowerup = powerupManager.GetRandomPowerup();
            
            bool shouldSpawnOnHit = Random.value > 0.5f;
            int durabilityOnHit = 0;
            if (shouldSpawnOnHit)
            {
                durabilityOnHit = Random.Range(DurabilityConstants.Durability1, DurabilityConstants.Durability6);
            }

            PowerupInfo info = new PowerupInfo(chosenPowerup, shouldSpawnOnHit, durabilityOnHit);
            brick.SetPowerupInfo(info);
        }
    }

    // Generate durability. Each successive increase is half as likely as the last (for balance reasons)
    private int CalculateDurability(int durability)
    {
        if (durability == DurabilityConstants.Durability6)
        {
            return DurabilityConstants.Durability6;
        }

        return Random.value > 0.5f ? CalculateDurability(durability + 1) : durability;
    }
}
