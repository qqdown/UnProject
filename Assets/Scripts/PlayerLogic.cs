using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerLogic : MonoBehaviour {

    public float MoveMultiplier= 3;
    public float RotateMultiplier = 1;

    private Bag bag = new Bag();
    private int san;

    public delegate void GameOverHandler();
    public event GameOverHandler GameOverEvent;

    private Vector3 m_Move;
    private Animation m_Anim;
    private CharacterController m_Controller;

    private void Start()
    {
        m_Anim = GetComponent<Animation>();
        m_Controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        m_Move = new Vector3(h, 0, v);
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
        UIManager.GetInst().ShowTip(string.Format("捡起物品【{0}】", item.GetName()));
    }

    public bool UseItem(Item.ItemId id, Item.ItemId need_id)
    {
        return bag.Consume(id, need_id);
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
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        if (GameOverEvent != null)
        {
            GameOverEvent();
        }
    }

}
