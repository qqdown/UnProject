using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
abstract public class Item : MonoBehaviour {

    public UnityEvent OnPickup = new UnityEvent();

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
    public HighlightingSystem.Highlighter highlighter;

    private void Start()
    {
        if (highlighter == null)
            highlighter = GetComponent<HighlightingSystem.Highlighter>();
        if (highlighter == null)
            highlighter = gameObject.AddComponent<HighlightingSystem.Highlighter>();

        collider_ = GetComponent<Collider>();
        collider_.isTrigger = true;
    }

    public void Highlight()
    {
        highlighter.On(Color.white);
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

    public virtual void OnPickedup()
    {
        owner_ = FindObjectOfType<PlayerLogic>();
        OnPickup.Invoke();
    }

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
    CAR_KEY,
    MOM_BAG,
    PARENT_ROOM_KEY,
    PHOTO1,
    PHOTO2,
    PHOTO3,
    PHOTO4
};