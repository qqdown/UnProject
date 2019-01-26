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
    public GameObject BagItemPrefab;
    public GameObject BagContent;

    public Button ButtonPickup;

    public string PlayScene = "Player";

    private Dictionary<Item, UIBagItem> bagItemDic = new Dictionary<Item, UIBagItem>();

    private PlayerLogic player
    {
        get
        {
            if (m_Player != null)
                return m_Player;
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            m_Player = go.GetComponent<PlayerLogic>();
            return m_Player;
        }
    }
    private PlayerLogic m_Player;

    private HashSet<Text> tipTextList = new HashSet<Text>();
    private Item itemForPickup = null;

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
        SceneManager.LoadScene(PlayScene);
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

    public void ShowPickup(bool shown, Item item = null)
    {
        if (shown)
        {
            Debug.Assert(item != null);
            ButtonPickup.gameObject.SetActive(true);
            itemForPickup = item;
        }
        else
        {
            ButtonPickup.gameObject.SetActive(false);
            itemForPickup = null;
        }
    }

    public void OnButtonPickupClick()
    {
        if(itemForPickup != null)
        {
            player.PickItem(itemForPickup);
            
            GameObject go = (GameObject)Instantiate(BagItemPrefab, BagContent.transform);
            go.transform.localEulerAngles = Vector3.one;
            UIBagItem ubi = go.GetComponent<UIBagItem>();
            bagItemDic.Add(itemForPickup, ubi);
            ubi.SetItem(itemForPickup);

            ShowPickup(false);
        }
    }

    public void OnRemoveItem(Item item)
    {
        var ubi = bagItemDic[item];
        bagItemDic.Remove(item);
        Destroy(ubi.gameObject);
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
