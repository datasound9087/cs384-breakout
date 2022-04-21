using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Superclass for a powerup.
*/
public abstract class Powerup
{
    // The related json details
    private PowerupProperty property;
    public string Name { get { return property.name; } }

    // Duration of this currently running powerup
    public int Duration { get; set; }

    // When did it start running
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

    // What should the powerup do when first started
    public virtual void Begin()
    {}

    // What should the powerup do each game update
    public virtual void Update()
    {}

    // What shoudl the powerup do when it finishes
    public virtual void End()
    {}
}