using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour {

    [SerializeField] private bool m_OpenedAtBeginning = false;
	// Use this for initialization
	void Start () {
        if (m_OpenedAtBeginning)
            Open();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
            Open();
	}

    public void Open()
    {
        transform.DORotate(new Vector3(0, 90, 0), 1, RotateMode.Fast);
    }

}
