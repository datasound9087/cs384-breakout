using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScreenPowerup : Powerup
{
    private Camera camera;
    public FlipScreenPowerup(PowerupProperty property, Camera camera)
        : base(property)
    {
        this.camera = camera;
    }

    public override void Begin()
    {
        camera.GetComponent<Transform>().Rotate(0, 0, 180);
    }
    public override void End()
    {
        camera.GetComponent<Transform>().Rotate(0, 0, -180);
    }
}