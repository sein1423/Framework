using Core;
using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePageManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void ReturnToMainPage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_MAINPAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }
}
