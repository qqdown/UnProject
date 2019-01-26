using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Item
{
    private bool used_;

    public Bear()
    {
        type_ = ItemType.CONSUME;
        used_ = false;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = false;
        if (CanUse())
        {
            used_ = true;
            return true;
        }
        return false;
    }

    public bool CanUse()
    {
        return !used_;
    }

}
