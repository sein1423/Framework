using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTable;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;
using JetBrains.Annotations;

public class StageSelectManager : MonoBehaviour
{
    public GameObject SelectStagePopup;
    int SelectStageNumber = 0;
    public ItemSO[] item;
    public TextMeshProUGUI[] costText;
    public BuyItemPanelController BuyItemPanel;

    private void Awake()
    {
        for(int i = 0; i < item.Length; i++)
        {
            costText[i].text = item[i].ItemCost.ToString();
        }
    }

    public void SelectStage(int StageNumber)
    {
        SelectStageNumber = StageNumber;
        SelectStagePopup.SetActive(true);
    }

    public void GoSelectStage()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_GAMEPAGE;
        Global.Instance.StageNumber = string.Format("GameStage{0:D2}", SelectStageNumber);
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }

    /*public void BackMain()
    {
        Global.Instance.NextScene = SceneTable.STR_SCENE_MAINPAGE;
        SceneManager.LoadScene(SceneTable.STR_SCENE_LODING, LoadSceneMode.Additive);
    }*/

    public void DisableStagePopup()
    {
        SelectStagePopup.SetActive(false);
    }

    public void BuyItem(int i)
    {
        BuyItemPanel.selectItem = item[i];
        BuyItemPanel.gameObject.SetActive(true);
    }
}

public class Item
{
    public int value;
    public string name;
    public int Count;
}
