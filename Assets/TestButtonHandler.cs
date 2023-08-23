using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;
    public ItemType[] itemTypes;
    private int lastItemIndex = 0;

    public void OnPointerClick(PointerEventData eventData) {
        var index = lastItemIndex;//Random.Range(0, itemTypes.Length);
        var itemType = itemTypes[index];
        inventoryManager.AddItem(itemType);
        lastItemIndex++;
        if (lastItemIndex == itemTypes.Length)
            lastItemIndex = 0;
    }
}
