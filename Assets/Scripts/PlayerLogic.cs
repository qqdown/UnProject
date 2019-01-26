using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    private Bag bag;
    private int san;

    public delegate void GameOverHandler();
    public event GameOverHandler GameOverEvent;

    public void PickItem(Item item)
    {
        bag.AddItem(item);
    }

    public bool UseItem(Item.ItemId id)
    {
        return bag.Consume(id);
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
