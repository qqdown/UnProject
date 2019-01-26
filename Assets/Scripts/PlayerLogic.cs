﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

public class PlayerLogic : MonoBehaviour {

    public float MoveMultiplier= 3;
    public float RotateMultiplier = 1;

    private Bag bag = new Bag();
    public int san =100;

    public UnityEvent GameOverEvent  = new UnityEvent();
    public bool AllowMove = true;
    public Transform StartPoint;

    private Vector3 m_Move;
    private Animation m_Anim;
    private CharacterController m_Controller;
    private bool car_key_used = false;
    private bool parent_key_used = false;

    private void Start()
    {
        
        m_Anim = GetComponent<Animation>();
        m_Controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (!AllowMove)
            m_Move = new Vector3(0, 0, 0);
        else
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            m_Move = new Vector3(h, 0, v);
        }
        Move(m_Move);
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        //move = transform.InverseTransformDirection(move);
        var m_TurnAmount = Mathf.Clamp(move.x, -1, 1) * Mathf.PI / 2.0f;
        var m_ForwardAmount = move.z;
        transform.Rotate(Vector3.up, m_TurnAmount * RotateMultiplier);
        var realMove = m_ForwardAmount * transform.forward * MoveMultiplier * Time.fixedDeltaTime;
        m_Controller.Move(realMove);
        if (move.magnitude <= float.Epsilon)
            m_Anim.CrossFade("idle");
        else
        {
            if (Mathf.Abs(m_ForwardAmount) < float.Epsilon)
                m_Anim["walk"].speed = 1;
            else
                m_Anim["walk"].speed = MoveMultiplier;
            m_Anim.CrossFade("walk");
        }
    }

    public void PickItem(Item item)
    {
        bag.PickupItem(item);
        
        UIManager.GetInst().ShowTip(string.Format("获得物品【{0}】", item.GetName()));
    }

    public void DropItem(Item item)
    {
        bag.DropItem(item.GetId());
    }

    public void UseItem(string id)
    {
        if (id == "CAR_KEY" && car_key_used)
            return;
        else if (id == "PARENT_ROOM_KEY" && parent_key_used)
            return;
        bool ret = bag.Consume((ItemId)Enum.Parse(typeof(ItemId), id));
        if (id == "CAR_KEY" && ret)
            car_key_used = true;
        else if (id == "PARENT_ROOM_KEY" && ret)
            parent_key_used = true;
    }

    public bool UseItemWithResult(ItemId id)
    {
        if (HasItem(id))
        {
            return bag.Consume(id);
        }
        return false;
    }

    public bool HasItem(ItemId id)
    {
        return bag.HasItem(id);
    }

    public void SanUp(int delta)
    {
        san += delta;
        if (san > 100)
        {
            san = 100;
        }
    }

    public void SanDown(int delta)
    {
        san -= delta;
        if (san <= 0)
        {
            var cg = UIManager.GetInst().FadeImage.GetComponent<CanvasGroup>();
            cg.DOFade(1, 0.5f).OnComplete(()=>
            {
                transform.position = StartPoint.position;
                san = 100;
                cg.DOFade(0, 0.5f);
            });
            OnGameOver();
        }
        Debug.Log(san);
    }

    private void OnGameOver()
    {
        if (GameOverEvent != null)
        {
            GameOverEvent.Invoke();
        }
    }

}
