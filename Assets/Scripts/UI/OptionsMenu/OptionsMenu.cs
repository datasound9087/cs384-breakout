using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Options Menu UI handler.
*/
public class OptionsMenu : MonoBehaviour
{
    public SoundManager soundManager;
    public Toggle soundToggle;
    private MenuSceneAnimator menuSceneAnimator;

    private void Awake()
    {
        menuSceneAnimator = GetComponent<MenuSceneAnimator>();
    }

    private void Start()
    {
        // Load muted information
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
