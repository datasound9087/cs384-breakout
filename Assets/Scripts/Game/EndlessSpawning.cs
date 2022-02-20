using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawning : IBrickSpawning
{
    private LevelManager levelManager;
    private static readonly float BRICK_SPAWN_CHANCE = 0.5f;
    public EndlessSpawning(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }
    public bool OnPlace(int x, int y)
    {
        float val = Random.value;
        if  (val > BRICK_SPAWN_CHANCE)
        {
            levelManager.NumBricksRemaining++;
            return true;
        }

        return false;
    }
    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        int durability = Random.Range(1, 6);
        brick.setDurability(durability);
    }
}
