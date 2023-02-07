using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using DataTable;
using UnityEngine.SceneManagement;
using System;

public class LoginManager : MonoBehaviour
{
    public ItemSO[] Items;
    private void Awake()
    {
        Global.Instance.NowScene = SceneTable.STR_SCENE_LOGIN;

    }

    private void Start()
    {
        Debug.Log("Start"); 
        SetNewData();
        Global.Instance.LoadUserData();
    }

    private void SetNewData()
    {
        Debug.Log("SetNewData");
        for(int i = 0; i < Items.Length; i++)
        {
            Debug.Log(Items[i].ToString());
            Global.Instance.ItemDict.Add(Items[i].ToString(), 0);
        }
        //Global.Instance.SaveData();
    }

    public void GoMainPage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_SELECTSTAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }
}
