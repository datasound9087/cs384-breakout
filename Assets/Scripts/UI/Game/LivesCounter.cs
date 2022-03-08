using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    private readonly string LIVES_TEXT = "Lives: ";
    private GameManager gameManager;
    
    // Cache lives so don't update UI every frame - text rendering is slow
    private int cachedLives;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        cachedLives = gameManager.GetLives();
        GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + cachedLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetLives() != cachedLives)
        {
            cachedLives = gameManager.GetLives();
            GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + cachedLives;
        }
    }
}
