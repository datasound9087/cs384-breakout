using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public GameSettings gameSettings;

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
