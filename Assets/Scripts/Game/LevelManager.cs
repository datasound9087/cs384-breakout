using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private bool atEndOfLevelChain = false;
    private Level loadedLevel;
    public LevelManager(string name)
    {
        loadedLevel = LevelLoader.LoadLevel(name);
        setIsLevelAtEndOfChain();
    }
    public void NextLevel()
    {
        if (!atEndOfLevelChain)
        {
            loadedLevel = LevelLoader.LoadLevel(loadedLevel.nextLevel);
            setIsLevelAtEndOfChain();
        }
    }

    public bool CanPlaceBrickAt(int x, int y)
    {
        if (x >= loadedLevel.width || y >= loadedLevel.height)
        {
            return false;
        }

        return loadedLevel.rowRefs[y][x] > 0;
    }

    public int GetNumberOfBreakableBricks()
    {
        return loadedLevel.numBreakableBricks;
    }

    public int GetLevelWidth()
    {
        return loadedLevel.width;
    }

    public int GetLevelHeight()
    {
        return loadedLevel.height;
    }

    public bool AtEndOFLevelChain()
    {
        return atEndOfLevelChain;
    }

    private void setIsLevelAtEndOfChain()
    {
        if (string.IsNullOrEmpty(loadedLevel.nextLevel))
        {
            atEndOfLevelChain = true;
        }
    }
}
