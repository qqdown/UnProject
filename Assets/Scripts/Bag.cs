using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag {

    private Dictionary<ItemId, Item> items_ = new Dictionary<ItemId, Item>();

    public void PickupItem(Item item)
    {
        items_.Add(item.GetId(), item);
        item.OnPickedup();
        item.gameObject.SetActive(false);
        UIManager.GetInst().OnPickupItem(item);
    }

    public void DropItem(ItemId id)
    {
        UIManager.GetInst().OnRemoveItem(GetItem(id));
        items_.Remove(id);
    }

    public Item GetItem(ItemId id)
    {
        return items_[id];
    }

    public Dictionary<ItemId, Item> GetAllItem()
    {
        return items_;
    }

    public bool HasItem(ItemId id)
    {
        return items_.ContainsKey(id);
    }

    public bool Consume(ItemId id)
    {
        if (!HasItem(id))
        {
            UIManager.GetInst().ShowTip(string.Format("缺少使用道具"));
            return false;
        }
        bool need_delete;
        var item = items_[id];
        bool ret = item.Consume(out need_delete);
        if (need_delete)
        {
            DropItem(id);
        }
        if (ret)
        {
            UIManager.GetInst().ShowTip(string.Format("【{0}】已被使用", item.GetName()));
        }
        return ret;
    }

}
