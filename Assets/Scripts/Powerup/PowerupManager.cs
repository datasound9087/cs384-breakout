using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private GameManager gameManager;
    private Camera gameCamera;
    private Paddle paddle;
    private Dictionary<string, PowerupProperty> powerupMap;
    private List<string> powerupKeys;
    private List<Powerup> activePowerups;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameCamera = FindObjectOfType<Camera>();
        paddle = FindObjectOfType<Paddle>();
        gameManager.OnBallDeath += this.Reset;

        powerupMap = new Dictionary<string, PowerupProperty>();
        activePowerups = new List<Powerup>();
        powerupKeys = new List<string>();
        LoadPowerups();
    }

    void FixedUpdate()
    {
        for (int i = activePowerups.Count - 1; i >= 0; i--)
        {
            Powerup powerup = activePowerups[i];
            powerup.Update();
            
            float now = Time.time;
            if (now - powerup.StartTime() > powerup.Duration)
            {
                powerup.End();
                activePowerups.RemoveAt(i);
            }
        }
    }

    public string GetRandomPowerup()
    {
        int val = Random.Range(0, powerupKeys.Count - 1);
        string name = powerupKeys[val];
        return name;
    }

    public void AddPowerup(string name)
    {
        if (!powerupMap.ContainsKey(name))
        {
            return;
        }

        Powerup powerup = CreatePowerupFromProperty(powerupMap[name]);
        powerup.Begin();
        activePowerups.Add(powerup);
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
            case "flipScreen": return new FlipScreenPowerup(property, gameCamera);
            case "paddleLength": return new PaddleLengthPowerup(property, paddle);
            default: return null;
        }
    }

    public void Reset()
    {
        foreach (Powerup powerup in activePowerups)
        {
            powerup.End();
        }
        activePowerups.Clear();
    }
}
