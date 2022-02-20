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

        foreach (PowerupProperty property in level.powerupLocations)
        {
            if (CanAddPowerup(x, y, property))
            {
                AddPowerup(brick, property);
            }
        }
    }

    private bool CanAddPowerup(int x, int y, PowerupProperty property)
    {
        return x == property.location[0] && y == property.location[1];
    }

    private void AddPowerup(Brick brick, PowerupProperty property)
    {
        PowerupInfo info = new PowerupInfo(property);
        brick.SetPowerupInfo(info);
    }
}
