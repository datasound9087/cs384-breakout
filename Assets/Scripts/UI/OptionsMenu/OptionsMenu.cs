using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public SoundManager soundManager;
    public Toggle soundToggle;
    private MenuSceneAnimator menuSceneAnimator;

    void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
    }

    void Start()
    {
        soundToggle.isOn = soundManager.GetMuted();
    }

    public void OnMuteSoundToggleChanged(bool changed)
    {
        soundManager.SetMuted(changed);
    }

    public void OnBackButtonClicked()
    {
        soundManager.PlaySound("MenuClick");
        menuSceneAnimator.TransitionToScene("MainMenu");
    }
}
