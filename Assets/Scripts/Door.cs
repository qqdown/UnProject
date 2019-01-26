using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour {

    [SerializeField] private bool m_OpenedAtBeginning = false;
    Vector3 initRotation;
    Vector3 targetRotation;
	// Use this for initialization
	void Start () {
        initRotation = transform.eulerAngles;
        targetRotation = initRotation + new Vector3(0, 90, 0);
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
        transform.DORotate(targetRotation, 1, RotateMode.Fast);
    }

}
