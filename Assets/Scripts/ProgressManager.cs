using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {

    public enum ProgressState {
        START,
        GET_TOOL,
        GET_BOOK_ROOM_NOTE,
        GET_SOFA_NOTE,
        GET_CAR_KEY,
        GET_PARENT_ROOM_KEY,
        WINDOW_OPEN,
        END
    };

    private static ProgressManager s_progressManager;
    private ProgressState state_;
    public delegate void StateChangeHandler(ProgressState now_state);
    public event StateChangeHandler StateEvent;

    public GameObject finalTrigger;
    public AudioClip waterAudio;

    public Item[] winItems;
    private int i = 0;

    private void Awake()
    {
        s_progressManager = this;
    }

    public static ProgressManager GetInst()
    {
        return s_progressManager;
    }

    private void Start()
    {
        foreach(var item in winItems){
            item.OnPickup.AddListener(()=>
            {
                i++;
                if(i == winItems.Length)
                {
                    finalTrigger.SetActive(true);
                }
            });
        }
    }

    public ProgressState GetState()
    {
        return state_;
    }

    public void SetState(ProgressState state)
    {
        if (state_ != state)
        {
            state_ = state;
            OnStateChange();
        }
    }

    public bool IsThroughState(ProgressState state)
    {
        return state_ >= state;
    }

    private void OnStateChange()
    {
        if (StateEvent != null)
        {
            StateEvent(state_);
        }
    }

    public void OnWin()
    {
        Debug.Log("OnWin");
    }

    public void Wash(Item newItem)
    {

        StartCoroutine(washHandler(newItem));
    }

    IEnumerator washHandler(Item newItem)
    {
        float time = 2.0f;
        UIManager.GetInst().ShowTip("清洗奖杯中", time);
        var player = FindObjectOfType<PlayerLogic>();
        player.AllowMove = false;
        Camera.main.gameObject.AddComponent<AudioSource>().PlayOneShot(waterAudio);
        yield return new WaitForSeconds(time);
        player.AllowMove = true;
        player.PickItem(newItem);
    }

}
