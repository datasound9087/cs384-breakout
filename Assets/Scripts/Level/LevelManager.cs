using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameObject brickArea;
    public int levelWidth;
    public int levelHeight;

    public event Action<string> OnLevelComplete;

    public int NumBricksRemaining { get; set; }
    private BrickSpawner brickSpawner;
    private string levelName;
    private Level loadedLevel;

    void Awake()
    {
        brickSpawner = FindObjectOfType<BrickSpawner>();
    }

    public void Start()
    {
        if (!gameSettings.endlessMode)
        {
            LoadLevelFromDisk("Level01");
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

    public int GetLevelWidth()
    {
        return levelWidth;
    }

    public int GetLevelHeight()
    {
        return levelHeight;
    }

    private void LoadLevelFromDisk(string name)
    {
        loadedLevel = LevelIO.LoadLevel(name);
        levelName = loadedLevel.name;
        levelWidth = loadedLevel.width;
        levelHeight = loadedLevel.height;
        NumBricksRemaining = loadedLevel.numBreakableBricks;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public bool LevelFinished()
    {
        bool levelComplete = NumBricksRemaining == 0;
        if (levelComplete)
        {
            OnLevelComplete(levelName);
        }

        return levelComplete;
    }

    public void ResetLevel()
    {
        ClearLevel();
        LoadLevel();
    }

    public void BrickDestroyed()
    {
        NumBricksRemaining--;
    }

    private void LoadLevel()
    {
        if (gameSettings.endlessMode)
        {
            UnityEngine.Random.InitState(gameSettings.endlessSettings.levelSeed);
            PowerupManager powerupManager = FindObjectOfType<PowerupManager>();
            brickSpawner.GenerateBricks(new EndlessSpawning(this, powerupManager));
        } else
        {
            brickSpawner.GenerateBricks(new LevelSpawning(loadedLevel));
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
