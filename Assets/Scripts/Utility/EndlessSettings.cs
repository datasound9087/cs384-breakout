using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptables/EndlessSettings")]
public class EndlessSettings : ScriptableObject
{
    public int levelSeed;
    public int levelRound;
}