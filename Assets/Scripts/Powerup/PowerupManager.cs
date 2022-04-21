using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Top level script which manages all powerups and their lifetimes.
*/
public class PowerupManager : MonoBehaviour
{
    private GameManager gameManager;
    private Paddle paddle;

    // Link a powerup string to it's associated properties
    private Dictionary<string, PowerupProperty> powerupMap;

    // List of powerup keys maintained for random generation
    private List<string> powerupKeys;

    // The active powerup objects, indexed by name
    private Dictionary<string, Powerup> activePowerups;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        paddle = FindObjectOfType<Paddle>();

        // Clear non persistent achievements on death
        gameManager.OnBallDeath += this.Reset;

        powerupMap = new Dictionary<string, PowerupProperty>();
        activePowerups = new Dictionary<string, Powerup>();
        powerupKeys = new List<string>();
        LoadPowerups();
    }

    private void FixedUpdate()
    {
        // powerup to remove at the of update as Dictionary's are immutable
        // (the chance of having to remove multiple per update is very low, in the worst case remove next update instead)
        Powerup powerupToRemove = null;
        foreach (var powerup in activePowerups)
        {
            Powerup p = powerup.Value;

            // Call the powerup's update method
            p.Update();
            
            // if the powerup has been active for its duration, schedule removal
            float now = Time.time;
            if (now - p.StartTime() > p.Duration)
            {
                powerupToRemove = p;
            }
        }

        // End powerup and remove from active powerups
        if (powerupToRemove != null)
        {
            powerupToRemove.End();
            activePowerups.Remove(powerupToRemove.Name);
        }
    }

    // Get a random powerup name
    // Used for randomly generated levels in endless mode
    public string GetRandomPowerup()
    {
        int val = Random.Range(0, powerupKeys.Count);
        string name = powerupKeys[val];
        return name;
    }

    public void AddPowerup(string name)
    {
        if (!powerupMap.ContainsKey(name))
        {
            return;
        }

        // Get relevant property for this powerup name
        PowerupProperty powerupProperty = powerupMap[name];
        if (activePowerups.ContainsKey(name))
        {
            // Powerup is already running
            // Update running powerup with longer duration
            Powerup powerup = activePowerups[name];
            powerup.Duration += powerupProperty.duration;
        }
        else
        {
            // Create relevant powerup behaviour from it's property, start it and add it to active powerups
            Powerup powerup = CreatePowerupFromProperty(powerupProperty);
            powerup.Begin();
            activePowerups.Add(name, powerup);
        }
    }

    private void LoadPowerups()
    {
        // Load properties from disk and store in maps/lists
        List<PowerupProperty> loadedPowerups = PowerupIO.LoadPowerups("Powerups");
        foreach (PowerupProperty powerup in loadedPowerups)
        {
            powerupMap.Add(powerup.name, powerup);
            powerupKeys.Add(powerup.name);
        }
    }

    private Powerup CreatePowerupFromProperty(PowerupProperty property)
    {
        // Create relevant behaviour for a powerup property
        // If a behaviour does not exist, has been configured incorrectly.
        // This could be made to be entirely defined in Json thorugh use of reflection
        switch (property.name)
        {
            case "paddleSpeed": return new PaddleSpeedPowerup(property, paddle);
            case "paddleLength": return new PaddleLengthPowerup(property, paddle);
            default: return null;
        }
    }

    public void Reset()
    {
        // Stop running powerups
        foreach (var powerup in activePowerups)
        {
            powerup.Value.End();
        }
        activePowerups.Clear();
    }
}
