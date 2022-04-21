using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Play Mode Menu UI handler.
*/
public class PlayMenu : MonoBehaviour
{
    public GameSettings gameSettings;
    public SoundManager soundManager;

    private MenuSceneAnimator sceneAnimator;

    private void Awake()
    {
        sceneAnimator = GetComponent<MenuSceneAnimator>();
    }

    public void OnLevelsButtonClicked()
    {
        gameSettings.endlessMode = false;
        soundManager.PlaySound("MenuClick");
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
