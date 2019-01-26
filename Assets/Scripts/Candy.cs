using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    public int buff_num_;

    public Candy()
    {
        type_ = ItemType.CONSUME;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = true;
        owner_.SanUp(buff_num_);
        return true;
    }
}
