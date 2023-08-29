using UnityEngine;

public class EquipItem : Item
{
    public Stats stats { get; private set; }

    public void InitializeRuntimeStats() {
        var base_stats = (itemType as EquipItemType).base_stats;
        stats = new Stats();
        stats.attack = base_stats.attack * Random.Range(0.8f, 1.2f);
        stats.defense = base_stats.defense * Random.Range(0.8f, 1.2f);
        stats.magic = base_stats.magic * Random.Range(0.8f, 1.2f);
    }


}
