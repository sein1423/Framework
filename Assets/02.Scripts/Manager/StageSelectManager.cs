using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTable;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public GameObject SelectStagePopup;
    int SelectStageNumber = 0;
    public void SelectStage(int StageNumber)
    {
        SelectStageNumber = StageNumber;
        //Debug.Log(Global.Instance.NextScene);
        SelectStagePopup.SetActive(true);
    }

    public void GoSelectStage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_GAMEPAGE;
        Global.Instance.StageNumber = string.Format("GameStage{0:D2}", SelectStageNumber);
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }

    public void BackMain()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_MAINPAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }
}
