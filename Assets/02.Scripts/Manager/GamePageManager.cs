using Core;
using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePageManager : MonoBehaviour
{
    UIManager uiManager;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        uiManager = UIManager.instance;
    }
    public void ReturnToMainPage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_MAINPAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        uiManager.SetPauseMenu(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        uiManager.SetPauseMenu(false);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneTable.STR_SCENE_GAMEPAGE);
        Time.timeScale = 1;
    }
}
