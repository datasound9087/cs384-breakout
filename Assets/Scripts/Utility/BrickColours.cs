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
            case DurabilityConstants.Durability1: return durability1;
            case DurabilityConstants.Durability2: return durability2;
            case DurabilityConstants.Durability3: return durability3;
            case DurabilityConstants.Durability4: return durability4;
            case DurabilityConstants.Durability5: return durability5;
            case DurabilityConstants.Durability6: return durability6;
            default: return unbreakable;
        }
    }
}