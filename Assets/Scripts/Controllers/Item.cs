using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image image;
    public ItemType itemType;
    public bool isGrabbed;
    public ItemContainer container;

    private void Update() {
        if (isGrabbed) {
            transform.position = Input.mousePosition;
        }
    }

    public bool Drop(ItemContainer newContainer) {
        if (newContainer.item == null) {
            if (newContainer.acceptedItemClass == ItemClass.all ||
                newContainer.acceptedItemClass == this.itemType.itemClass) {
                
                newContainer.PutItem(this);
                isGrabbed = false;
                return true;
            }
        }
        return false;
    }

    public bool Grab() {
        if (isGrabbed) { return false; }

        container.TakeItem();

        isGrabbed = true;
        return true;
    }

    private void Start() {
        image.sprite = itemType.sprite;
    }
}
