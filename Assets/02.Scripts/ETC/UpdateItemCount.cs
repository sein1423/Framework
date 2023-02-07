using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class UpdateItemCount : MonoBehaviour
{
    public TextMeshProUGUI[] itemCounter;

    public void UpdateItemCounter()
    {
        for(int i = 0; i < itemCounter.Length; i++)
        {
            Debug.Log(StageSelectManager.Instance.item[i]);
            Debug.Log(Global.Instance.ItemDict[StageSelectManager.Instance.item[i].ToString()]);
            itemCounter[i].text = Global.Instance.ItemDict[StageSelectManager.Instance.item[i].ToString()].ToString();
        }
    }
}
