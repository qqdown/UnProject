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
        owner_.AddSan(buff_num_);
        return true;
    }
}
