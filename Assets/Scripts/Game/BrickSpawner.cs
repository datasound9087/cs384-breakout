using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // For Func delegate

public class BrickSpawner : MonoBehaviour
{
    // Game settings to see if should be spawning levels or generating them
    public GameSettings gameSettings;
    // Brick prefab asset
    public GameObject brick;
    public float wallWorldAreaRangeX;
    public float wallWorldAreaRangeY;
    public float gapBetweenBricks;
    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    /*
        Generate bricks into the world of the correct size and shape. 
        spawnRules - IBrickSpawning which provides spawning rules and other brick initialising logic.
    */
    public void GenerateBricks(IBrickSpawning spawnRules)
    {
        // Calculate size of bricks relative to area
        float brickWorldSizeX = 2.0f * wallWorldAreaRangeX / (float)levelManager.GetLevelWidth();
        float brickWorldSizeY = 2.0f * wallWorldAreaRangeY / (float)levelManager.GetLevelHeight();
        
        //get half of brick size for positioning
        float brickWorldSizeXHalf = brickWorldSizeX / 2.0f;

        // Start at top left in world coords to spawn bricks
        float startX = -wallWorldAreaRangeX - brickWorldSizeXHalf;
        float startY = -wallWorldAreaRangeY;

        // Initial scale of brick is 1
        // Work out needed scale to get calculated desired size
        // This is so that loaded levels of different sizes can scale to board size
        Vector3 brickSize = brick.GetComponent<Renderer>().bounds.size;
        Vector3 brickScale = brick.transform.localScale;

        float xScale = brickWorldSizeX * brickScale.x / brickSize.x;
        float yScale = brickWorldSizeY * brickScale.y / brickSize.y;

        Vector3 brickScale2 = new Vector3(xScale, yScale, 0);

        for (int y = 0; y < levelManager.GetLevelHeight(); y++)
        {
            for (int x = 0; x < levelManager.GetLevelWidth(); x++)
            {
                if (spawnRules.OnPlace(x, y))
                {
                    Vector3 pos = new Vector3(startX, startY, 0);
                    Brick go = Instantiate(brick, pos, Quaternion.identity, this.transform).GetComponent<Brick>();
                    go.transform.localScale = brickScale2;
                    
                    spawnRules.OnBrickInitialise(x, y, go);
                }
                startX += brickWorldSizeX + gapBetweenBricks;
            }
            startY += brickWorldSizeY + gapBetweenBricks;
            startX = -wallWorldAreaRangeX - brickWorldSizeXHalf;
        }
    }
}
