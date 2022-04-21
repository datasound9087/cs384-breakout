using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Levels mode brick generation.
*/
public class LevelSpawning : IBrickSpawning
{
    // The level data to use
    private Level level;
    private const int AIR_ID = 0;

    public LevelSpawning(Level level)
    {
        this.level = level;
    }
    
    public bool OnPlace(int x, int y)
    {
        // If position is out of range of this levels data
        if (x >= level.width || y >= level.height)
        {
            return false;
        }

        // Yes as long as this brick is not air (ignored)
        return level.rowRefs[y][x] != AIR_ID;
    }

    public void OnBrickInitialise(int x, int y, Brick brick)
    {
        // Get durability from level
        int durability = level.rowRefs[y][x];
        brick.SetDurability(durability);

        // Add powerup if it exists at this location
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
        // Set brick powerup info
        PowerupInfo info = new PowerupInfo(location);
        brick.SetPowerupInfo(info);
    }
}
