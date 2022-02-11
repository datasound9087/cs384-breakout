using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int durability { get; set; } = 1;
    public bool unbreakable { get; set; } = false;

    BrickManager brickManager;

    void Start()
    {
        brickManager = FindObjectOfType<BrickManager>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            durability--;
            if (!unbreakable && durability == 0)
            {
                brickManager.BrickDestroyed();
                Destroy(this.gameObject);
            }
        }
    }
}
