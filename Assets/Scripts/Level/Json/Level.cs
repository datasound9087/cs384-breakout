using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Level and its associated data.
*/
[System.Serializable]
public class Level
{
    // Display name
    public string name;

    // Json file containing next level (no file extension)
    public string nextLevel;

    // Level dimensions (ignored currently due to Unity not containing a full Json parser implementation (level size is hardcoded by class serialisation))
    public int width;
    public int height;
    public LevelRow levelRows;

    // Brick powerup data
    public List<BrickPowerupLocation> powerupLocations;

    // Below here values are calculated internally - mainly for ease of use 
    // For ease of use

    // The number of breakable bricks in the level
    [System.NonSerialized] 
    public int numBreakableBricks;

    // Row references to make indexing into level data easier
    [System.NonSerialized]
    public List<int>[] rowRefs;

    // Is level at the end of the chain
    [System.NonSerialized]
    public bool endOfChain;
}
