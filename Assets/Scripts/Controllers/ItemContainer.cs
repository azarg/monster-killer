using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item item;
    public ItemClass acceptedItemClass;

    public void PutItem(Item item) {
        item.transform.SetParent(transform, worldPositionStays: false);
        item.container = this;
        this.item = item;
    }

    public void TakeItem() {
        item.container = null;
        item.transform.SetParent(transform.root);
        item = null;
    }
}
