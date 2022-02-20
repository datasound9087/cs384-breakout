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
        // Generate seed for level generation
        gameSettings.endlessSettings.levelSeed = Random.Range(int.MinValue, int.MaxValue);

        SceneManager.LoadSceneAsync("Game");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
