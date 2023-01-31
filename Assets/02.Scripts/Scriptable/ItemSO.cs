using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite ItemImage;
    public int ItemCost;
    [TextArea]
    public string ItemText;
}