using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using DataTable;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void StageSelect()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_SELECTSTAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }

}
