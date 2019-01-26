using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {
 
    private static UIManager s_uiManager;

    public GameObject PanelStart;
    public GameObject PanelTip;
    public GameObject PanelPlay;

    public GameObject TipPrefab;

    private HashSet<Text> tipTextList = new HashSet<Text>();

    public static UIManager GetInst()
    {
        return s_uiManager;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        s_uiManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShowTip(Time.time.ToString(), 3);
    }

    public void OnButtonStartClick()
    {
        SceneManager.LoadScene("Play");
        PanelStart.SetActive(false);
        PanelPlay.SetActive(true);
    }

    public void OnButtonExitClick()
    {
        Application.Quit();
    }

    public void ShowTip(string message, float showTime = 3.0f)
    {
        StartCoroutine(ShowTipHandler(message, showTime));
    }

    public void ShowImage(Texture texture)
    {

    }

    private IEnumerator ShowTipHandler(string message, float showTime)
    {
        GameObject go = (GameObject)Instantiate(TipPrefab, PanelTip.transform);
        Text text = go.GetComponent<Text>();
        go.transform.localScale = Vector3.one;
        RectTransform rt = go.transform as RectTransform;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (PanelTip.transform as RectTransform).rect.width);
        text.text = message;
        yield return new WaitForSeconds(showTime);
        Color c = text.color;
        c.a = 0;
        (text as Graphic).DOColor(c, 0.5f).OnComplete(()=> Destroy(go));
    }
}
