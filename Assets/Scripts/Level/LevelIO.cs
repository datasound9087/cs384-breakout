using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIO
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
            CalcNumberOfBricks(loadedLevel);

            // add row references for easier access into level data
            AddRowReferences(loadedLevel);

            // Is this level at the end of the chain?
            SetIsLevelAtEndOfChain(loadedLevel);
        }

        return loadedLevel;
    }

    private static void CalcNumberOfBricks(Level level)
    {
        int numBreakableBricks = 0;

        numBreakableBricks += SumRow(level.levelRows.row1);
        numBreakableBricks += SumRow(level.levelRows.row2);
        numBreakableBricks += SumRow(level.levelRows.row3);
        numBreakableBricks += SumRow(level.levelRows.row4);
        numBreakableBricks += SumRow(level.levelRows.row5);
        numBreakableBricks += SumRow(level.levelRows.row6);
        numBreakableBricks += SumRow(level.levelRows.row7);
        numBreakableBricks += SumRow(level.levelRows.row8);
        numBreakableBricks += SumRow(level.levelRows.row9);
        numBreakableBricks += SumRow(level.levelRows.row10);
        numBreakableBricks += SumRow(level.levelRows.row11);
        numBreakableBricks += SumRow(level.levelRows.row12);
        numBreakableBricks += SumRow(level.levelRows.row13);
        numBreakableBricks += SumRow(level.levelRows.row14);
        numBreakableBricks += SumRow(level.levelRows.row15);

        level.numBreakableBricks = numBreakableBricks;
    }

    // add row references for easier access into level data
    private static int SumRow(List<int> row)
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

    private static void AddRowReferences(Level loadedLevel)
    {
        loadedLevel.rowRefs = new List<int>[] {
            loadedLevel.levelRows.row15,
            loadedLevel.levelRows.row14,
            loadedLevel.levelRows.row13,
            loadedLevel.levelRows.row12,
            loadedLevel.levelRows.row11,
            loadedLevel.levelRows.row10,
            loadedLevel.levelRows.row9,
            loadedLevel.levelRows.row8,
            loadedLevel.levelRows.row7,
            loadedLevel.levelRows.row6,
            loadedLevel.levelRows.row5,
            loadedLevel.levelRows.row4,
            loadedLevel.levelRows.row3,
            loadedLevel.levelRows.row2,
            loadedLevel.levelRows.row1
        };
    }

    private static void SetIsLevelAtEndOfChain(Level loadedLevel)
    {
        loadedLevel.endOfChain = string.IsNullOrEmpty(loadedLevel.nextLevel);
    }
}
