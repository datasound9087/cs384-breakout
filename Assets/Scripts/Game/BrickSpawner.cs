using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spawn Bricks into the scene.
*/
public class BrickSpawner : MonoBehaviour
{
    // Brick prefab asset
    public GameObject brick;

    // Dimensions of the area that bricks can be spawned in
    public float wallWorldAreaRangeX;
    public float wallWorldAreaRangeY;

    // Gap between each spawned brick
    public float gapBetweenBricks;

    private LevelManager levelManager;
    private ScoreManager scoreManager;
    private AchievementManager achievementManager;
    private SoundManager soundManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        achievementManager = FindObjectOfType<AchievementManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    
    /*
        Generate bricks into the world of the correct size and shape. 
        spawnRules - Object which provides spawning rules and other brick generation logic.
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
        // This is so that loaded levels of different sizes can scale to board size (be handy in future when Json parsing is better!)
        Vector3 brickSize = brick.GetComponent<Renderer>().bounds.size;
        Vector3 brickScale = brick.transform.localScale;

        float xScale = brickWorldSizeX * brickScale.x / brickSize.x;
        float yScale = brickWorldSizeY * brickScale.y / brickSize.y;

        // resultant brick size
        Vector3 brickScale2 = new Vector3(xScale, yScale, 0);

        for (int y = 0; y < levelManager.GetLevelHeight(); y++)
        {
            for (int x = 0; x < levelManager.GetLevelWidth(); x++)
            {   
                // Can a brick be placed there according to given rules
                if (spawnRules.OnPlace(x, y))
                {
                    // Create brick at position and size
                    Vector3 pos = new Vector3(startX, startY, 0);
                    Brick go = Instantiate(brick, pos, Quaternion.identity, this.transform).GetComponent<Brick>();
                    go.transform.localScale = brickScale2;
                    
                    // Notify achievment and level on destroy
                    go.OnBreak += levelManager.BrickDestroyed;
                    go.OnBreak += achievementManager.OnBrickDestroy;

                    // Notify score and achievment on durability change
                    go.OnDurabilityChange += scoreManager.IncrementScore;
                    go.OnDurabilityChange += achievementManager.OnBrickHit;

                    // Play a sound when hit
                    go.OnHit += () => soundManager.PlaySound("BallHit");
                    
                    // Run other generation logic
                    spawnRules.OnBrickInitialise(x, y, go);
                }
                startX += brickWorldSizeX + gapBetweenBricks;
            }
            startY += brickWorldSizeY + gapBetweenBricks;
            startX = -wallWorldAreaRangeX - brickWorldSizeXHalf;
        }
    }
}
