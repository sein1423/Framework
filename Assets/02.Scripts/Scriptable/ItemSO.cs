using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/ItemSO")]
[System.Serializable]
public class ItemSO : ScriptableObject
{
    public Sprite ItemImage;
    public int ItemCost;
    [TextArea]
    public string ItemText;
}