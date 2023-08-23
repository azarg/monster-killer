using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;
    public ItemType[] itemTypes;

    public void OnPointerClick(PointerEventData eventData) {
        var index = Random.Range(0, itemTypes.Length);
        var itemType = itemTypes[index];
        inventoryManager.AddItem(itemType);
    }
}
