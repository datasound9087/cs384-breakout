using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Script to manage level lifetimes throughout the game.
*/
public class LevelManager : MonoBehaviour
{
    public GameSettings gameSettings;

    // The top level object which holds all the bricks in the scene
    public GameObject brickArea;
    public int levelWidth;
    public int levelHeight;

    // Event fired on level completion. string - completed level name
    public event Action<string> OnLevelComplete;

    // Bricks remaining before level completion
    public int NumBricksRemaining { get; set; }

    private BrickSpawner brickSpawner;
    private string levelName;

    // The loaded level Json, if applicable
    private Level loadedLevel;

    private void Awake()
    {
        brickSpawner = FindObjectOfType<BrickSpawner>();
    }

    private void Start()
    {
        // Load level from disk if needed
        if (!gameSettings.endlessMode)
        {
            LoadLevelFromDisk("Level01");
        }

        LoadLevel();
    }

    public void NextLevel()
    {
        // Load next level from disk if applicable
        if (!gameSettings.endlessMode)
        {
            if (!loadedLevel.endOfChain)
            {
                LoadLevelFromDisk(loadedLevel.nextLevel);
            }
        }
        else
        {
            // Increment level reached
            gameSettings.endlessSettings.levelRound++;
        }

        ClearLevel();
        LoadLevel();
    }

    private void LoadLevelFromDisk(string name)
    {
        // Load level and get relevant metadata
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

    public bool LevelComplete()
    {
        return NumBricksRemaining == 0;
    }

    public void ResetLevel()
    {
        // Clear and reload current level
        ClearLevel();
        LoadLevel();
    }

    // Callback for when a brick is destroyed in the level
    public void BrickDestroyed()
    {
        NumBricksRemaining--;
        if (LevelComplete())
        {
            // Level complete - fire event
            OnLevelComplete(levelName);
        }
    }

    public void Restart()
    {
        ResetLevel();
    }

    public int GetLevelWidth()
    {
        return levelWidth;
    }

    public int GetLevelHeight()
    {
        return levelHeight;
    }

    public bool EndOfLevels()
    {
        return loadedLevel.endOfChain;
    }

    private void LoadLevel()
    {
        if (gameSettings.endlessMode)
        {
            // In endless mode level name is just a number
            levelName = "" + gameSettings.endlessSettings.levelRound;

            // Initialsie random gen with seed. This could mean in future level seeds could be shared as generation is repeatable.
            UnityEngine.Random.InitState(gameSettings.endlessSettings.levelSeed);

            // Spawn level using random mechanics
            PowerupManager powerupManager = FindObjectOfType<PowerupManager>();
            brickSpawner.GenerateBricks(new EndlessSpawning(this, powerupManager));
        } else
        {
            // Spawn level using level data
            brickSpawner.GenerateBricks(new LevelSpawning(loadedLevel));
        }
    }

    private void ClearLevel()
    {
        // Delete all bricks from scene
        foreach (Transform brick in brickArea.GetComponent<Transform>())
        {
            GameObject.Destroy(brick.gameObject);
        }
    }
}
