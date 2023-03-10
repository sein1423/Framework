using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 2;
    public GameObject slotPrefab;
    public GameObject slotPanel;

    private void Awake()
    {
        for (int i = 0; i<maxSlot; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name = "Slot_" + i;
            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
        }
    }
}
