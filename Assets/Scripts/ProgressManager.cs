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

    private void Awake()
    {
        s_progressManager = this;
    }

    public static ProgressManager GetInst()
    {
        return s_progressManager;
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

}
