using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickPowerupLocation
{
    public string name;
    public List<int> location;
    public bool onHit;
    public int onDurability;
}