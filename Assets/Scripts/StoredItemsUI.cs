using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredItemsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject storedItemFab;
    [SerializeField]
    private GameObject parentPanel;

    private Dictionary<InventoryItemSO, StoredItemUI> itemUIs = new Dictionary<InventoryItemSO, StoredItemUI>();
    
    private void OnEnable()
    {
        StoredItems.onChangedStoredItems += UpdateUI;
    }
    private void OnDisable()
    {
        StoredItems.onChangedStoredItems -= UpdateUI;
    }

    private void UpdateUI(Dictionary<InventoryItemSO, int> itemsInStorage)
    {
        foreach(var item in itemsInStorage)
        {
            if (!itemUIs.ContainsKey(item.Key))
            {
                GameObject newUI = Instantiate(storedItemFab, parentPanel.transform);
                itemUIs.Add(item.Key, newUI.GetComponent<StoredItemUI>());
                itemUIs[item.Key].SetImage(item.Key);
            }
            itemUIs[item.Key].SetText(item.Value.ToString());
        }
    }
}
