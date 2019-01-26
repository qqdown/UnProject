using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item {

    public enum ItemId
    {
        TOOL,
        BOOK_ROOM_NOTE,
        BEAR,
        SOFA_NOTE,
        CANDY,
        CAT_KEY,
        PARENT_ROOM_KEY,
        PHOTO
    };

    public enum ItemType
    {
        TARGET,
        KEY,
        CONSUME,
        TOOL,
        NOTE,
    };

    public ItemId id_;
    public string name_;
    public string msg_;
    public ItemType type_;
    protected PlayerLogic owner_;

    public ItemId GetId()
    {
        return id_;
    }

    public string GetName()
    {
        return name_;
    }

    public string GetMsg()
    {
        return msg_;
    }

    public void SetOwner(PlayerLogic owner)
    {
        owner_ = owner;
    }

    abstract public bool Consume(out bool need_delete);
	
}
