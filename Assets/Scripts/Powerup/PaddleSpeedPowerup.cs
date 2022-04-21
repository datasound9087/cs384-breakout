using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSpeedPowerup : Powerup
{
    private Paddle paddle;
    public PaddleSpeedPowerup(PowerupProperty property, Paddle paddle)
        : base(property)
    {
        this.paddle = paddle;
    }

    public override void Begin()
    {
        paddle.IncreaseSpeed();
    }
    public override void End()
    {
        paddle.DecreaseSpeed();
    }
}