using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private GameManager gameManager;
    private Camera gameCamera;
    private Paddle paddle;
    private Dictionary<string, PowerupProperty> powerupMap;
    private List<Powerup> activePowerups;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameCamera = FindObjectOfType<Camera>();
        paddle = FindObjectOfType<Paddle>();

        powerupMap = new Dictionary<string, PowerupProperty>();
        activePowerups = new List<Powerup>();
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
}
