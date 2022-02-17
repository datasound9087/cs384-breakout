using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader
{
    public static Level LoadLevel(string name)
    {
        Level loadedLevel = null;
        // Load json level file from disk into objects
        TextAsset ob = (TextAsset)Resources.Load("Levels/" + name, typeof(TextAsset));
        loadedLevel = JsonUtility.FromJson<Level>(ob.ToString());

        if (loadedLevel != null)
        {
            // calculate number of bricks in level
            calcNumberOfBricks(loadedLevel);

            // add row references for easier access into level data
            addRowReferences(loadedLevel);
        }

        return loadedLevel;
    }

    private static void calcNumberOfBricks(Level level)
    {
        int numBreakableBricks = 0;

        numBreakableBricks += sumRow(level.levelRows.row1);
        numBreakableBricks += sumRow(level.levelRows.row2);
        numBreakableBricks += sumRow(level.levelRows.row3);
        numBreakableBricks += sumRow(level.levelRows.row4);
        numBreakableBricks += sumRow(level.levelRows.row5);
        numBreakableBricks += sumRow(level.levelRows.row6);
        numBreakableBricks += sumRow(level.levelRows.row7);
        numBreakableBricks += sumRow(level.levelRows.row8);
        numBreakableBricks += sumRow(level.levelRows.row9);
        numBreakableBricks += sumRow(level.levelRows.row10);
        numBreakableBricks += sumRow(level.levelRows.row11);
        numBreakableBricks += sumRow(level.levelRows.row12);
        numBreakableBricks += sumRow(level.levelRows.row13);
        numBreakableBricks += sumRow(level.levelRows.row14);
        numBreakableBricks += sumRow(level.levelRows.row15);

        level.numBreakableBricks = numBreakableBricks;
    }

    // add row references for easier access into level data
    private static int sumRow(List<int> row)
    {
        int sum = 0;
        foreach (int r in row)
        {
            if (r > 0)
            {
                sum++;
            }
        }

        return sum;
    }

    private static void addRowReferences(Level loadedLevel)
    {
        loadedLevel.rowRefs = new List<int>[] {
            loadedLevel.levelRows.row1,
            loadedLevel.levelRows.row2,
            loadedLevel.levelRows.row3,
            loadedLevel.levelRows.row4,
            loadedLevel.levelRows.row5,
            loadedLevel.levelRows.row6,
            loadedLevel.levelRows.row7,
            loadedLevel.levelRows.row8,
            loadedLevel.levelRows.row9,
            loadedLevel.levelRows.row10,
            loadedLevel.levelRows.row11,
            loadedLevel.levelRows.row12,
            loadedLevel.levelRows.row13,
            loadedLevel.levelRows.row14,
            loadedLevel.levelRows.row15
        };
    }
}
