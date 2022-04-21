using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Brick layout data for a level
 * Due to Unity's Json parser, this is hardcoded by class serialisation to the 15 rows below.
*/
[System.Serializable]
public class LevelRow
{
    public List<int> row1;
    public List<int> row2;
    public List<int> row3;
    public List<int> row4;
    public List<int> row5;
    public List<int> row6;
    public List<int> row7;
    public List<int> row8;
    public List<int> row9;
    public List<int> row10;
    public List<int> row11;
    public List<int> row12;
    public List<int> row13;
    public List<int> row14;
    public List<int> row15;
}