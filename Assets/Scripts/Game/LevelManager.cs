using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameObject brickArea;
    public int levelWidth;
    public int levelHeight;

    private BrickSpawner brickSpawner;
    private string levelName;
    private int numBricksRemaining;
    private Level loadedLevel;
    private int levelSeed;
    private static readonly float BRICK_SPAWN_CHANCE = 0.5f;

    void Awake()
    {
        brickSpawner = FindObjectOfType<BrickSpawner>();
    }

    public void Start()
    {
        if (!gameSettings.endlessMode)
        {
            LoadLevelFromDisk("Level01");
        } else
        {
            // Generate level seed
            levelSeed = Random.Range(0, int.MaxValue);
        }

        LoadLevel();
    }

    public void NextLevel()
    {
        if (!gameSettings.endlessMode)
        {
            if (!loadedLevel.endOfChain)
            {
                LoadLevelFromDisk(loadedLevel.nextLevel);
            }
        }

        ClearLevel();
        LoadLevel();
    }
    public bool CanPlaceBrickForLevel(int x, int y)
    {
        if (x >= loadedLevel.width || y >= loadedLevel.height)
        {
            return false;
        }

        return loadedLevel.rowRefs[y][x] > 0;
    }

    public bool CanPlaceBrickForSeed(int x, int y)
    {
        float val = Random.value;
        if  (val > BRICK_SPAWN_CHANCE)
        {
            numBricksRemaining++;
            return true;
        }

        return false;
    }

    public int GetNumberOfBricksRemaining()
    {
        return numBricksRemaining;
    }

    public int GetLevelWidth()
    {
        return levelWidth;
    }

    public int GetLevelHeight()
    {
        return levelHeight;
    }

    public int GetLevelSeed()
    {
        return levelSeed;
    }

    private void LoadLevelFromDisk(string name)
    {
        loadedLevel = LevelLoader.LoadLevel(name);
        levelName = loadedLevel.name;
        levelWidth = loadedLevel.width;
        levelHeight = loadedLevel.height;
        numBricksRemaining = loadedLevel.numBreakableBricks;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public bool LevelFinished()
    {
        return numBricksRemaining == 0;
    }

    public void ResetLevel()
    {
        ClearLevel();
        LoadLevel();
    }

    public void BrickDestroyed()
    {
        numBricksRemaining--;
    }

    private void LoadLevel()
    {
        if (gameSettings.endlessMode)
        {
            Random.InitState(levelSeed);
            brickSpawner.GenerateBricks(this.CanPlaceBrickForSeed, null);
        } else
        {
            brickSpawner.GenerateBricks(this.CanPlaceBrickForLevel, null);
        }
    }

    private void ClearLevel()
    {
        foreach (Transform brick in brickArea.GetComponent<Transform>())
        {
            GameObject.Destroy(brick.gameObject);
        }
    }
}
