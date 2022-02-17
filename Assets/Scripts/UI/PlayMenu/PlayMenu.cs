using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public GameSettings gameSettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelsButtonClicked()
    {
        gameSettings.endlessMode = false;
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnEndlessButtonClicked()
    {
        gameSettings.endlessMode = true;
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
