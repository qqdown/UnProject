using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIBagItem : MonoBehaviour {

    private Image image;
    private Item item;
    public GameObject messagePanel;
    public Text messageText;

    public void SetItem(Item item)
    {
        this.item = item;
        if (image == null)
            image = GetComponent<Image>();
        image.sprite = item.texture;
        messageText.text = item.GetMsg();
    }

    public void ShowMessage()
    {
        messagePanel.SetActive(true);
        if (item.texture)
        {
            UIManager.GetInst().LargeImage.gameObject.SetActive(true);
            UIManager.GetInst().LargeImage.sprite = item.texture;
            UIManager.GetInst().LargeImage.rectTransform.DOScale(Vector3.one, 0.2f);
        }
    }

    public void HideMessage()
    {
        UIManager.GetInst().LargeImage.rectTransform.DOScale(Vector3.zero, 0.2f).OnComplete(()=>
        {
            messagePanel.SetActive(false);
            UIManager.GetInst().LargeImage.gameObject.SetActive(false);
        });
    }
}
