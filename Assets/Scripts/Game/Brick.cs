using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public BrickColours brickColours;
    private bool unbreakable = false;
    private int durability = 1;

    private LevelManager levelManager;
    private ScoreManager scoreManager;
    private SpriteRenderer brickSprite;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        brickSprite = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            scoreManager.IncrementScore();
            durability--;
            if (!unbreakable && durability == 0)
            {
                levelManager.BrickDestroyed();
                Destroy(this.gameObject);
            } else
            {
                UpdateColour();
            }
        }
    }

    public void setDurability(int durability)
    {
        this.durability = durability;
        if (this.durability < 0)
        {
            unbreakable = true;
        }

        UpdateColour();
    }

    private void UpdateColour()
    {
        brickSprite.color = brickColours.GetColourForDurability(durability);
        Debug.Log(brickSprite.color);
    }
}
