using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    public int buff_num_;
    private Object owner_;

    public void SetOwner(Object owner)
    {
        owner_ = owner;
    }

    public override bool Consume(out bool need_delete)
    {
        need_delete = true;
        return true;
    }
}
