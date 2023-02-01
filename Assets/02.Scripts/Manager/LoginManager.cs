using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using DataTable;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private void Awake()
    {
        Global.Instance.NowScene = SceneTable.STR_SCENE_LOGIN;

    }

    private void Start()
    {
        Global.Instance.LoadUserData();
    }

    public void GoMainPage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_SELECTSTAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }
}
