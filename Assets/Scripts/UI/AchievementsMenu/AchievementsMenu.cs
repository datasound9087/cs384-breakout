using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementsMenu : MonoBehaviour
{
    private MenuSceneAnimator menuSceneAnimator;

    void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
    }
    public void OnBackButtonClicked()
    {
        menuSceneAnimator.TransitionToScene("MainMenu");
    }
}
