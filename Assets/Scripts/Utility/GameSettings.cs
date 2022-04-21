using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Scriptable to move game settings between scenes.
*/
[CreateAssetMenu(menuName = "MyScriptables/GameSettings")]
public class GameSettings : ScriptableObject
{
    // Is game running in endless levels mode
    public bool endlessMode;
    
    // How many lives should each level start with
    public int startingLives;

    // Endless sub-settings
    public EndlessSettings endlessSettings;
}

