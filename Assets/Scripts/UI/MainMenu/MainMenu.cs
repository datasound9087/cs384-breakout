using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pressToStartText;
    public GameObject OptionsPanel;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pressToStartText.SetActive(false);
            OptionsPanel.SetActive(true);
        }
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadSceneAsync("PlayMenu");
    }

    public void OnOptionsButtonClicked()
    {
        SceneManager.LoadSceneAsync("OptionsMenu");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
