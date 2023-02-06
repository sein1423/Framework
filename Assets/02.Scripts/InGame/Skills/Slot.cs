using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    QuickSlot quickSlot;
    GameManager gameManager;
    public int num;

    private void Start()
    {
        gameManager = GameManager.instance;
        quickSlot = gameManager.Player.GetComponentInChildren<QuickSlot>();
        num = int.Parse(gameObject.name.Substring(gameObject.name.IndexOf("_") + 1));
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            quickSlot.slots[num].isEmpty = true;
        }
    }
}
