using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private ScreenManager screenManager;

    void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }

    public void SwitchPanel(int index)
    {
        screenManager.SwitchPanel(index);
        AudioManager.Instance.PlaySFX(0);
    }

    public void LoadScene(string sceneName)
    {
        screenManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        screenManager.QuitGame();
    }

    public void LoadLevel(string level)
    {
        screenManager.LoadLevel(level);
    }

}