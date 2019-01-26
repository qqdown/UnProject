using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
abstract public class Item : MonoBehaviour {
   

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
    public bool canPickup;
    public Sprite texture;
    protected PlayerLogic owner_;

    protected Collider collider_;

    private void Start()
    {
        collider_ = GetComponent<Collider>();
        collider_.isTrigger = true;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.GetInst().ShowPickup(true, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.GetInst().ShowPickup(false);
        }
    }

}

[Serializable]
public enum ItemId
{
    NOTE1,
    NOTE2,
    BEAR,
    CANDY,
    THOPHY_DIRTY,
    THOPHY_CLEAN,
    DAD_BOX,
    CAT_KEY,
    MOM_BAG,
    PARENT_ROOM_KEY,
    PHOTO1,
    PHOTO2,
    PHOTO3,
    PHOTO4
};