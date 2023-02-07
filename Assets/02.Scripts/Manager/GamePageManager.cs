using Core;
using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePageManager : MonoBehaviour
{
    UIManager uiManager;
    public GameObject TutorialImage;
    private void Awake()
    {
        uiManager = UIManager.instance;
        Application.targetFrameRate = 60;
        /*
        
        if (Global.Instance.StageNumber == "GameStage01" ||
            Global.Instance.StageNumber == "GameStage02" ||
            Global.Instance.StageNumber == "GameStage03" ||
            Global.Instance.StageNumber == "GameStage04" ||
            Global.Instance.StageNumber == "GameStage05" ||
            Global.Instance.StageNumber == "GameStage06")
        {
            TutorialImage.SetActive(true);
            Time.timeScale = 0;
        }
        else TutorialImage.SetActive(false);*/
    }
    public void ReturnToMainPage()
    {
        Time.timeScale = 1;
        Global.Instance.NextScene = SceneTable.STR_SCENE_SELECTSTAGE;
        SceneManager.UnloadSceneAsync(Global.Instance.StageNumber);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneTable.STR_SCENE_GAMEPAGE);
    }

    public void CompTutorial()
    {
        TutorialImage.SetActive(false);
        Time.timeScale = 1;
    }
}
