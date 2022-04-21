using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawning : IBrickSpawning
{
    private LevelManager levelManager;
    private PowerupManager powerupManager;
    private const float BrickSpawnChance = 0.2f;
    private const float BrickUnbreakableChance = 0.1f;
    public EndlessSpawning(LevelManager levelManager, PowerupManager powerupManager)
    {
        this.levelManager = levelManager;
        this.powerupManager = powerupManager;
    }

    public bool OnPlace(int x, int y)
    {
        float val = Random.value;
        if  (val < BrickSpawnChance)
        {
            levelManager.NumBricksRemaining++;
            return true;
        }

        return false;
    }
    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        if (Random.value < BrickUnbreakableChance)
        {
            brick.setDurability(DurabilityConstants.Unbreakable);
        }
        else
        {
            brick.setDurability(CalculateDurability(DurabilityConstants.Durability1));
        }

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

    private int CalculateDurability(int durability)
    {
        if (durability == DurabilityConstants.Durability6)
        {
            return DurabilityConstants.Durability6;
        }

        return Random.value > 0.5f ? CalculateDurability(durability + 1) : durability;
    }
}
