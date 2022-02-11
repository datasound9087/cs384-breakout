using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    // Brick prefab asset
    public GameObject brick;
    public float wallWorldAreaRangeX = 5.0f;
    public float wallWorldAreaRangeY = 5.0f;
    public int wallWidth = 10;
    public int wallHeight = 10;
    private int remainingBricks = 100;

    private Brick[,] bricks;
    void Start()
    {
        generateWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (remainingBricks == 0)
        {
            // WOOOOOP
        }
    }

    public void BrickDestroyed()
    {
        remainingBricks--;
    }

    private void generateWall()
    {
        bricks = new Brick[wallWidth, wallHeight];

        // Calculate size of bricks relative to area
        float brickWorldSizeX = 2.0f * wallWorldAreaRangeX / (float)wallWidth;
        float brickWorldSizeY = 2.0f * wallWorldAreaRangeY / (float)wallHeight;
        
        // Start at top left in world coords to spawn bricks
        float startX = -wallWorldAreaRangeX;
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
            startX += brickWorldSizeX + 0.1f;

            if (i > 0 && i % wallWidth == 0)
            {
                startY += brickWorldSizeY + 0.1f;
                startX = -wallWorldAreaRangeX;
            }
        }
    }
}
