using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Load powerup json from resources.
*/
public class PowerupIO
{
    public static List<PowerupProperty> LoadPowerups(string name)
    {
        PowerupJSON loadedPowerups = null;

        // Load powerups file from disk into objects
        TextAsset ob = (TextAsset)Resources.Load("Powerups/" + name, typeof(TextAsset));
        loadedPowerups = JsonUtility.FromJson<PowerupJSON>(ob.ToString());

        return loadedPowerups.powerups;
    }
}