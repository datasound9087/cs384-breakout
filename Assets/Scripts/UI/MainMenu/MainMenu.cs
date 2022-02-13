using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pressToStartText;
    public GameObject OptionsPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
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
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
