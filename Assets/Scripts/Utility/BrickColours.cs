using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptables/BrickColours")]
public class BrickColours : ScriptableObject
{
    public Color unbreakable;
    public Color durability1;
    public Color durability2;
    public Color durability3;
    public Color durability4;
    public Color durability5;
    public Color durability6;

    public Color GetColourForDurability(int durability)
    {
        switch (durability)
        {
            case 1: return durability1;
            case 2: return durability2;
            case 3: return durability3;
            case 4: return durability4;
            case 5: return durability5;
            case 6: return durability6;
            default: return unbreakable;
        }
    }
}