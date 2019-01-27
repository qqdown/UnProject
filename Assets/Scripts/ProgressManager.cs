using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.Cameras;

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

    public Transform camStartTrans;
    public Transform camEndTrans;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnWin();
            
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

    public void OnWin()
    {
        Camera cam = Camera.main;
        AutoCam ac = FindObjectOfType<AutoCam>();
        ac.enabled = false;
        FindObjectOfType<ProtectCameraFromWallClip>().enabled = false;
        //cam.transform.SetParent(null);
        cam.transform.position = camStartTrans.position;
        cam.transform.rotation = camStartTrans.rotation;
        cam.transform.DOMove(camEndTrans.position, 3);
        cam.transform.DORotateQuaternion(camEndTrans.rotation, 3).OnComplete(()=>
        {
            StartCoroutine(AutoRotate(cam));
        });
    }

    IEnumerator AutoRotate(Camera cam)
    {
        float time = 15.0f;
        float startTime = Time.time;
        UIManager.GetInst().ProducterImage.gameObject.SetActive(true);
        while ((Time.time - startTime) <= time)
        {
            cam.transform.Rotate(Vector3.up, 450f / time * Time.fixedDeltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

}
