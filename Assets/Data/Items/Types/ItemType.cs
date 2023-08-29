using UnityEngine;


[CreateAssetMenu(menuName = "Item/Base item")]
public class ItemType : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public ItemClass itemClass;
    public GameObject prefab;

    public virtual Item CreateItem() {
        var itemGameObject = Instantiate(this.prefab);
        var item = itemGameObject.GetComponent<Item>();
        item.itemType = this;
        item.image.preserveAspect = true;
        return item;
    }
}
