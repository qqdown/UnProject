using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class RaycastThroughTransparent : MonoBehaviour {


    private CanvasGroup m_CanvasGroup;

	// Use this for initialization
	void Start () {
        m_CanvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_CanvasGroup.alpha <= 0.0001)
            m_CanvasGroup.blocksRaycasts = false;
        else
            m_CanvasGroup.blocksRaycasts = true;
	}
}
