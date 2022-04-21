using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public GameSettings gameSettings;
    public SoundManager soundManager;

    private MenuSceneAnimator sceneAnimator;

    void Awake()
    {
        sceneAnimator = GetComponent<MenuSceneAnimator>();
    }

    public void OnLevelsButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameSettings.endlessMode = false;
        sceneAnimator.TransitionToScene("Game");
    }

    public void OnEndlessButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        gameSettings.endlessMode = true;
        // Generate seed for level generation
        gameSettings.endlessSettings.levelSeed = Random.Range(int.MinValue, int.MaxValue);

        sceneAnimator.TransitionToScene("Game");
    }

    public void OnBackButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        sceneAnimator.TransitionToScene("MainMenu");
    }
}
