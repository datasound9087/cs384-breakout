using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneAnimator : MonoBehaviour
{
    public Animator sceneAnimator;
    public int waitTime = 1;

    public void TransitionToScene(string name)
    {
        StartCoroutine(ToNextScene(name));
    }

    private IEnumerator ToNextScene(string name)
    {
        sceneAnimator.SetTrigger("SceneOut");

        // Time.timeScale independent version of WaitForSeconds. Therefore transitions can run independently of timeScale
        // Required if the game is paused (Time.timeScale = 0)
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadSceneAsync(name);
    }
}