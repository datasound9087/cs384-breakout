using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Lives UI handler.
*/
public class LivesCounter : MonoBehaviour
{
    private readonly string LIVES_TEXT = "Lives: ";
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnBallDeath += this.OnLivesChanged;
        gameManager.OnRestart += this.OnLivesChanged;
    }

    private void Start()
    {   
        OnLivesChanged();
    }
    
    // Display lives
    public void OnLivesChanged()
    {
        GetComponent<TextMeshProUGUI>().text = LIVES_TEXT + gameManager.GetLives();
    }
}
