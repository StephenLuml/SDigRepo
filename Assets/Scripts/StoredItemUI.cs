using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoredItemUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private TextSetter textSetter;

    public void SetText(string text)
    {
        if (!textSetter)
        {
            textSetter = GetComponent<TextSetter>();
        }
        textSetter.SetText(text);
    }

    public void SetImage(InventoryItemSO item)
    {
        image.sprite = item.sprite;
    }
}
