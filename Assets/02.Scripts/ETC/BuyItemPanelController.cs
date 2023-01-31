using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;

public class BuyItemPanelController : MonoBehaviour
{
    public ItemSO selectItem = null;
    public Image ItemSprite;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemText;
    public TextMeshProUGUI ItemCost;

    private void OnEnable()
    {
        ItemSprite.sprite = selectItem.ItemImage;
        ItemName.text = selectItem.name;
        ItemText.text = selectItem.ItemText;
        ItemCost.text = selectItem.ItemCost.ToString();
    }

    public void BuyItem()
    {
        if(Global.Instance.Coin < selectItem.ItemCost)
        {
            return;
        }

        Global.Instance.Coin -= selectItem.ItemCost;
        Global.Instance.ItemDict[selectItem]++; 
        Global.Instance.SaveData();
        gameObject.SetActive(false);

    }
}
