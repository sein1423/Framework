using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using DataTable;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void StartGame()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_GAMEPAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }

}
