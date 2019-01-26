using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

    public Door door_;

    public Key()
    {
        type_ = ItemType.KEY;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = false;
        door_.Open();
        return true;
    }

}
