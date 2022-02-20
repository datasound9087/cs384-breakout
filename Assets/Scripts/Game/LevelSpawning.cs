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
    }
}
