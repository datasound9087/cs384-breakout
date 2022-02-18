using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int durability { get; set; } = 1;
    public bool unbreakable { get; set; } = false;

    LevelManager levelManager;
    ScoreManager scoreManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            durability--;
            if (!unbreakable && durability == 0)
            {
                levelManager.BrickDestroyed();
                scoreManager.IncrementScore();
                Destroy(this.gameObject);
            }
        }
    }
}
