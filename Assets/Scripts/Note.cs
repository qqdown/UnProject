using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Item
{

    public override bool Consume(out bool need_delete)
    {
        need_delete = false;
        return true;
    }
}
