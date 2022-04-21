using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    private PowerupProperty property;
    public int Duration { get; set; }
    public string Name { get { return property.name; } }
    private float startTime;
    public Powerup(PowerupProperty property)
    {
        this.property = property;
        this.startTime = Time.time;
        this.Duration = property.duration;
    }

    public float StartTime()
    {
        return startTime;
    }

    public virtual void Begin()
    {}
    public virtual void Update()
    {}
    public virtual void End()
    {}
}