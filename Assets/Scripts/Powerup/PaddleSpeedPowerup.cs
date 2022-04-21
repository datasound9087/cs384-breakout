using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Behaviour implementation of the paddle speed powerup.
*/
public class PaddleSpeedPowerup : Powerup
{
    private Paddle paddle;
    public PaddleSpeedPowerup(PowerupProperty property, Paddle paddle)
        : base(property)
    {
        this.paddle = paddle;
    }

    // When activated increase speed
    public override void Begin()
    {
        paddle.IncreaseSpeed();
    }

    // At end go back to normal
    public override void End()
    {
        paddle.DecreaseSpeed();
    }
}