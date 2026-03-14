using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSpecificSceneTransition(int indexScene)
    {
        Time.timeScale = 1;
        Transition.Instance.BeginTransition();
        StartCoroutine(WaitLoadScene(indexScene));
    }

    private IEnumerator WaitLoadScene(int scene)
    {
        yield return new WaitForSeconds(1.6f);
        LoadSpecificSceneWithDelay(scene);
    }

    public void LoadSpecificSceneWithDelay(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
    
    public void LoadSpecificScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void Pause(Canvas canvasPause)
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        canvasPause.enabled = (canvasPause.enabled) ? false : true;
    }

    public void SwitchSpeedsHigh()
    {
        Time.timeScale = (Time.timeScale == 1) ? 2 : 1;
    }

    public void SetTimeScaleSpeed(int _speed)
    {
        Time.timeScale = _speed;
    }
}
