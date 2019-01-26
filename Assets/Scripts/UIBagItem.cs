using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagItem : MonoBehaviour {

    private Image image;
	

    public void SetItem(Item item)
    {
        if (image == null)
            image = GetComponent<Image>();
        image.sprite = item.texture;
    }
}
