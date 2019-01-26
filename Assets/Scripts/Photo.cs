using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : Item
{

    public Photo()
    {
        type_ = ItemType.TARGET;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = false;
        return true;
    }
}
