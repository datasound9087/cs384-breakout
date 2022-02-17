using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // For Func delegate

public class BrickManager : MonoBehaviour
{
    // Game settings to see if should be spawning levels or generating them
    public GameSettings gameSettings;
    // Brick prefab asset
    public GameObject brick;
    public float wallWorldAreaRangeX;
    public float wallWorldAreaRangeY;
    public int wallWidth;
    public int wallHeight;
    public float gapBetweenBricks;

    private int remainingBricks;

    private LevelManager levelManager;

    void Start()
    {
        if (gameSettings.endlessMode)
        {
            remainingBricks = wallWidth * wallHeight;
            generateBricks(generateWall, null);
        } else
        {
            levelManager = new LevelManager("Level01");
            wallWidth = levelManager.GetLevelWidth();
            wallWidth = levelManager.GetLevelHeight();
            remainingBricks = levelManager.GetNumberOfBreakableBricks();
            generateBricks(generateLevel, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BrickDestroyed()
    {
        remainingBricks--;
    }

    public int RemaingBricks()
    {
        return remainingBricks;
    }

    private bool generateWall(int x, int y)
    {
        return true;
    }

    private bool generateLevel(int x, int y)
    {
        return levelManager.CanPlaceBrickAt(x, y);
    }

    private void generateBricks(Func<int, int, bool> brickSpawnFunc, Action<Brick> initialiseBrickFunc)
    {
        // Calculate size of bricks relative to area
        float brickWorldSizeX = 2.0f * wallWorldAreaRangeX / (float)wallWidth;
        float brickWorldSizeY = 2.0f * wallWorldAreaRangeY / (float)wallHeight;
        
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

        for (int y = 0; y < wallHeight; y++)
        {
            for (int x = 0; x < wallWidth; x++)
            {
                if (brickSpawnFunc(x, y))
                {
                    Vector3 pos = new Vector3(startX, startY, 0);
                    Brick go = Instantiate(brick, pos, Quaternion.identity, this.transform).GetComponent<Brick>();
                    go.transform.localScale = brickScale2;
                    if (initialiseBrickFunc != null)
                    {
                        initialiseBrickFunc(go);
                    }
                }
                startX += brickWorldSizeX + gapBetweenBricks;
            }
            startY += brickWorldSizeY + gapBetweenBricks;
            startX = -wallWorldAreaRangeX - brickWorldSizeXHalf;
        }
    }
    public void Reset()
    {
        if (gameSettings.endlessMode)
        {
            remainingBricks = wallWidth * wallHeight;
            generateBricks(generateWall, null);
        } else
        {
            levelManager.NextLevel();
            wallWidth = levelManager.GetLevelWidth();
            wallWidth = levelManager.GetLevelHeight();
            remainingBricks = levelManager.GetNumberOfBreakableBricks();
            generateBricks(generateLevel, null);
        }
    }
}
