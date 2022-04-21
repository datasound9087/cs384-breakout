using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Behaviour implementation of a paddle length powerup.
*/
public class PaddleLengthPowerup : Powerup
{
    private Paddle paddle;
    private Vector3 originalScale;
    public PaddleLengthPowerup(PowerupProperty property, Paddle paddle)
        : base(property)
    {
        this.paddle = paddle;
    }

    // When powerup activated, increase paddle size
    public override void Begin()
    {
        originalScale = paddle.transform.localScale;
        float newScaleX = originalScale.x * 2.0f;
        paddle.transform.localScale = new Vector3(newScaleX, originalScale.y, originalScale.z);
    }

    // Go back to origional size when finished
    public override void End()
    {
        paddle.transform.localScale = originalScale;
    }
}