using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawning : IBrickSpawning
{
    private Level level;

    public LevelSpawning(Level level)
    {
        this.level = level;
    }
    public bool OnPlace(int x, int y)
    {
        if (x >= level.width || y >= level.height)
        {
            return false;
        }

        return level.rowRefs[y][x] > 0;
    }
    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        int durability = level.rowRefs[y][x];
        brick.setDurability(durability);

        foreach (BrickPowerupLocation location in level.powerupLocations)
        {
            if (CanAddPowerup(x, y, location))
            {
                AddPowerup(brick, location);
            }
        }
    }

    private bool CanAddPowerup(int x, int y, BrickPowerupLocation location)
    {
        return x == location.location[0] && y == location.location[1];
    }

    private void AddPowerup(Brick brick, BrickPowerupLocation location)
    {
        PowerupInfo info = new PowerupInfo(location);
        brick.SetPowerupInfo(info);
    }
}
