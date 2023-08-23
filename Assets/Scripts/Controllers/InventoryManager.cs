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
                var item = CreateItem(itemType);
                slots[i].PutItem(item);
                break;
            }
        }
    }

    private Item CreateItem(ItemType itemType) {
        var itemGameObject = Instantiate(itemPrefab);
        var item = itemGameObject.GetComponent<Item>();
        item.itemType = itemType;
        item.image.preserveAspect = true;
        return item;
    }
}
