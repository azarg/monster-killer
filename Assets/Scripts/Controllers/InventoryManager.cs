using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemContainer[] slots;
    public GameObject itemPrefab;

    public void AddItem(ItemType itemType) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].item == null) { 
                var item = itemType.CreateItem();
                slots[i].PutOrSwapItem(item);
                break;
            }
        }
    }
}
