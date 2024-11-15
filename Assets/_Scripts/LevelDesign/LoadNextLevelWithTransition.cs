using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelWithTransition : MonoBehaviour
{
    [SerializeField] private string _levelName;

    public void LoadLevel()
    {
        StartCoroutine(LoadOnTime());
    }
    private IEnumerator LoadOnTime()
    {
        Transition.instance.TransitionOut();
        yield return new WaitForSecondsRealtime(Transition.instance.TimeTransitioning+0.5f);
        SceneManager.LoadScene(_levelName);
    }
}
