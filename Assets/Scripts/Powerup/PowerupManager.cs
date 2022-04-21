using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private GameManager gameManager;
    private Paddle paddle;
    private Dictionary<string, PowerupProperty> powerupMap;
    private List<string> powerupKeys;
    private Dictionary<string, Powerup> activePowerups;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        paddle = FindObjectOfType<Paddle>();
        gameManager.OnBallDeath += this.Reset;

        powerupMap = new Dictionary<string, PowerupProperty>();
        activePowerups = new Dictionary<string, Powerup>();
        powerupKeys = new List<string>();
        LoadPowerups();
    }

    void FixedUpdate()
    {
        Powerup powerupToRemove = null;
        foreach (var powerup in activePowerups)
        {
            Powerup p = powerup.Value;
            p.Update();
            
            float now = Time.time;
            if (now - p.StartTime() > p.Duration)
            {
                powerupToRemove = p;
            }
        }

        if (powerupToRemove != null)
        {
            powerupToRemove.End();
            activePowerups.Remove(powerupToRemove.Name);
        }
    }

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

        PowerupProperty powerupProperty = powerupMap[name];
        if (activePowerups.ContainsKey(name))
        {
            // Update running powerup with longer duration
            Powerup powerup = activePowerups[name];
            powerup.Duration += powerupProperty.duration;
        }
        else
        {
            Powerup powerup = CreatePowerupFromProperty(powerupProperty);
            powerup.Begin();
            activePowerups.Add(name, powerup);
        }
    }

    private void LoadPowerups()
    {
        List<PowerupProperty> loadedPowerups = PowerupLoader.LoadPowerups("Powerups");
        foreach (PowerupProperty powerup in loadedPowerups)
        {
            powerupMap.Add(powerup.name, powerup);
            powerupKeys.Add(powerup.name);
        }
    }

    private Powerup CreatePowerupFromProperty(PowerupProperty property)
    {
        switch (property.name)
        {
            case "paddleSpeed": return new PaddleSpeedPowerup(property, paddle);
            case "paddleLength": return new PaddleLengthPowerup(property, paddle);
            default: return null;
        }
    }

    public void Reset()
    {
        foreach (var powerup in activePowerups)
        {
            powerup.Value.End();
        }
        activePowerups.Clear();
    }
}
