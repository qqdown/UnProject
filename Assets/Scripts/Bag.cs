using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag {

    private Dictionary<Item.ItemId, Item> items_ = new Dictionary<Item.ItemId, Item>();

    public void PickupItem(Item item)
    {
        items_.Add(item.GetId(), item);
        item.gameObject.SetActive(false);
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

    public bool Consume(Item.ItemId id, Item.ItemId need_id)
    {
        if (id != need_id)
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
