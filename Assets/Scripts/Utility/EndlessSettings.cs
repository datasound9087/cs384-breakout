using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Scriptable for endless mode settings between scenes.
*/
[CreateAssetMenu(menuName = "MyScriptables/EndlessSettings")]
public class EndlessSettings : ScriptableObject
{
    // Level seed
    public int levelSeed;

    // What level are we currently on
    public int levelRound;
}