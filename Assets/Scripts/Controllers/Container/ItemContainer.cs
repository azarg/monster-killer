using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item item { get; private set; }

    [SerializeField] private ItemClass acceptedItemClass;

    public virtual Item TakeItem() {
        Item returnValue = null;
        if (item != null) {
            returnValue = item;
            item.TakeOutOfContainer();
            item = null;
        }
        return returnValue;
    }

    public virtual Item PutOrSwapItem(Item newItem) {
        Item returnValue = null;
        if (this.Accepts(newItem.itemType.itemClass)) {
            if(this.HasItem()) {
                returnValue = this.item;
                this.item.TakeOutOfContainer();
                newItem.PutInContainer(this);
                this.item = newItem;
            }
            else {
                newItem.PutInContainer(this);
                this.item = newItem;
            }
        }
        return returnValue;
    }

    private bool HasItem() {
        return item != null;
    }

    private bool Accepts(ItemClass itemClass) {
        return acceptedItemClass == ItemClass.all || acceptedItemClass == itemClass;
    }
}
