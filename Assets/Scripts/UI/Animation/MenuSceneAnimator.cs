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
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(name);
    }
}