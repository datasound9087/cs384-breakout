using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
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
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnEndlessButtonClicked()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
