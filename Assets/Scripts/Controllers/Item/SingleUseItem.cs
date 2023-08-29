using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUseItem : Item
{
    public override void HandleUpdate() {
        // do nothing.  these items cannot be moved
    }

    public override void TakeOutOfContainer() {
        UseItem();
    }

    public virtual void UseItem() {
        Destroy(this);
    }
}
