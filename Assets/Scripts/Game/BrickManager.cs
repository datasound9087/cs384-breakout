using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    // Brick prefab asset
    public GameObject brick;
    public float wallWorldAreaRangeX;
    public float wallWorldAreaRangeY;
    public int wallWidth;
    public int wallHeight;
    public float gapBetweenBricks;
    private int remainingBricks;

    void Start()
    {
        generateWall();
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

    private void generateWall()
    {
        remainingBricks = wallWidth * wallHeight;

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

        for (int i = 1; i <= wallWidth * wallHeight; i++)
        {
            Vector3 pos = new Vector3(startX, startY, 0);
            Brick go = Instantiate(brick, pos, Quaternion.identity, this.transform).GetComponent<Brick>();
            go.transform.localScale = brickScale2;
            startX += brickWorldSizeX + gapBetweenBricks;

            if (i > 0 && i % wallWidth == 0)
            {
                startY += brickWorldSizeY + gapBetweenBricks;
                startX = -wallWorldAreaRangeX - brickWorldSizeXHalf;
            }
        }
    }

    public void Reset()
    {
        generateWall();
    }
}
