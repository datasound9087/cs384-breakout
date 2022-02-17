using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public string nextLevel;
    public int width;
    public int height;
    public LevelRow levelRows;

    // Below here values are calculated internally - mainly for ease of use 
    // For ease of use
    [System.NonSerialized] 
    public int numBreakableBricks;

    // Row references to make indexing into level data easier
    [System.NonSerialized]
    public List<int>[] rowRefs;
}
