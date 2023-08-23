using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemClass { none, all, potion }

[CreateAssetMenu]
public class ItemType : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public ItemClass itemClass;
}
