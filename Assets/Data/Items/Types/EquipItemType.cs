using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Equippable item")]
public class EquipItemType : ItemType
{
    public Stats base_stats;

    public override Item CreateItem() {
        var item = (EquipItem)base.CreateItem();
        item.InitializeRuntimeStats();
        return item;
    }
}
