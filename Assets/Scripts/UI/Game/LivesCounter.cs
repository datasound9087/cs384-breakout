using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    private readonly string LIVES_TEXT = "Lives: ";
    private GameManager gameManager;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnBallDeath += this.UpdateText;
    }

    void Start()
    {
        UpdateText();
    }
    
    public void UpdateText()
    {
        GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + gameManager.GetLives();
    }
}
