using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Codebox : MonoBehaviour {

    public string code = "1234";
    public UnityEvent OnCodeSuccess = new UnityEvent();

	public void EnterCode(string code)
    {
        if (CheckCode(code))
        {
            OnCodeSuccess.Invoke();
        }
    }

    public bool CheckCode(string code)
    {
        return code == this.code;
    }

    public void ShowCodeUI()
    {
        UIManager.GetInst().ButtonShowCode.gameObject.SetActive(true);
        UIManager.GetInst().ButtonShowCode.onClick.RemoveAllListeners();
        UIManager.GetInst().ButtonShowCode.onClick.AddListener(()=>
        {
            UIManager.GetInst().ShowCodeBoxUI(this);
            UIManager.GetInst().ButtonShowCode.gameObject.SetActive(false);
        });
    }

    public void HideCodeUI()
    {
        UIManager.GetInst().ButtonShowCode.gameObject.SetActive(false);
    }
}
