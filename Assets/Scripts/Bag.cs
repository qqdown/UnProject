using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag {
    
    private Dictionary<Item.ItemId, Item> items_;

    public void AddItem(Item item)
    {
        items_[item.GetId()] = item;
    }

    public void DropItem(Item.ItemId id)
    {
        items_.Remove(id);
    }

    public Item GetItem(Item.ItemId id)
    {
        return items_[id];
    }

    public Dictionary<Item.ItemId, Item> GetAllItem()
    {
        return items_;
    }

    public bool HasItem(Item.ItemId id)
    {
        return items_.ContainsKey(id);
    }

    public bool Consume(Item.ItemId id)
    {
        if (!HasItem(id))
        {
            return false;
        }
        bool need_delete;
        bool ret = items_[id].Consume(out need_delete);
        if (need_delete)
        {
            DropItem(id);
        }
        return ret;
    }

}
