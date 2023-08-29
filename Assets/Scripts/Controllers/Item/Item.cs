using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public Image image;
    public ItemType itemType;
    public bool isGrabbed;
    public ItemContainer container;

    private void Start() {
        image.sprite = itemType.sprite;
    }

    private void Update() {
        HandleUpdate();
    }

    public virtual void HandleUpdate() {
        if (isGrabbed) {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void TakeOutOfContainer() {
        this.container = null;
        this.transform.SetParent(transform.root);
        isGrabbed = true;
    }

    public virtual void PutInContainer(ItemContainer container) {
        this.container = container;
        this.transform.SetParent(container.transform);
        isGrabbed = false;
    }
}
