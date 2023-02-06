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
    public static StageSelectManager Instance;

    public GameObject SelectStagePopup;
    int SelectStageNumber = 0;
    public ItemSO[] item;
    public TextMeshProUGUI[] costText;
    public BuyItemPanelController BuyItemPanel;
    public bool[] ItemUse = new bool[6] {false, false, false, false, false, false};
    public TextMeshProUGUI StageText;

    public TextMeshProUGUI profileStar;
    public TextMeshProUGUI profileCoin;
    public TextMeshProUGUI profileStage;
    public TextMeshProUGUI ShopCoin;
    public UpdateItemCount[] scripts; 

    public int[,] text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        for(int i = 0; i < item.Length; i++)
        {
            costText[i].text = item[i].ItemCost.ToString();
        }
        Global.Instance.ItemUse = ItemUse;
    }

    public void SelectStage(int StageNumber)
    {
        SelectStageNumber = StageNumber;
        StageText.text = $"Stage {StageNumber}";
        SelectStagePopup.SetActive(true);
    }

    public void GoSelectStage()
    {
        Global.Instance.ItemUse = ItemUse;
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
        UpdateProfileNShop();
        BuyItemPanel.gameObject.SetActive(true);
    }

    public void SelectStageWithItem(int i)
    {
        if (ItemUse[i])
        {
            ItemUse[i] = false;
            Global.Instance.ItemDict[item[i].ToString()]++;
        }
        else
        {
            if (Global.Instance.ItemDict[item[i].ToString()] < 1)
            {
                Debug.Log("아이템의 소지 개수가 부족합니다");
                return;
            }

            ItemUse[i] = true;
            Global.Instance.ItemDict[item[i].ToString()]--;
        }
    }

    public void UpdateProfileNShop()
    {
        profileStar.text = Global.Instance.Star.ToString();
        profileCoin.text = Global.Instance.Coin.ToString();
        profileStage.text = Global.Instance.Stage.ToString();
        ShopCoin.text = Global.Instance.Coin.ToString();
        UpdateItemCounter();
    }

    public void UpdateItemCounter()
    {
        for(int i = 0; i < scripts.Length; i++)
        {
            scripts[i].UpdateItemCounter();
        }
    }
}


public class Item
{
    public int value;
    public string name;
    public int Count;
}
