using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawning : IBrickSpawning
{
    private LevelManager levelManager;
    private PowerupManager powerupManager;
    private const float BrickSpawnChance = 0.5f;
    public EndlessSpawning(LevelManager levelManager, PowerupManager powerupManager)
    {
        this.levelManager = levelManager;
        this.powerupManager = powerupManager;
    }
    public bool OnPlace(int x, int y)
    {
        float val = Random.value;
        if  (val > BrickSpawnChance)
        {
            levelManager.NumBricksRemaining++;
            return true;
        }

        return false;
    }
    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        int durability = Random.Range(DurabilityConstants.Unbreakable, DurabilityConstants.Durability6);
        brick.setDurability(durability);

        float shouldSpawnPowerup = Random.value;
        if (shouldSpawnPowerup > 0.8f)
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
}
