using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemContainer : ItemContainer
{
    public override Item PutOrSwapItem(Item newItem) {
        var _item = base.PutOrSwapItem(newItem);
        GameManager.Instance.player.RecalculateStats();
        return _item;
    }

    public override Item TakeItem() {
        var _item = base.TakeItem();
        GameManager.Instance.player.RecalculateStats();
        return _item;
    }
}
