using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    public Shelf shelf_;

    public Tool()
    {
        type_ = ItemType.TOOL;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = false;
        var item = shelf_.DropItem();
        owner_.PickItem(item);
        return true;
    }
}
