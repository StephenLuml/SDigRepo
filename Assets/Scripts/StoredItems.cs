using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredItems : MonoBehaviour
{
    public static StoredItems Instance;

    private Dictionary<InventoryItemSO, int> storedItems = new Dictionary<InventoryItemSO, int>();

    public static Action<Dictionary<InventoryItemSO, int>> onChangedStoredItems;

    private void OnEnable()
    {
        PlayerInventory.onCollectedItems += AddToStorage;
        ShopButtons.onPurchaseUpgrade += TryToUpgrade;
    }
    private void OnDisable()
    {
        PlayerInventory.onCollectedItems -= AddToStorage;
        ShopButtons.onPurchaseUpgrade -= TryToUpgrade;
    }

    private void Start()
    {
        Instance = this;
    }

    private void TryToUpgrade(UpgradeType upgrade)
    {
        switch (upgrade)
        {
            case UpgradeType.Drill:
                break;
            case UpgradeType.Energy:
                break;
        }
    }

    private void AddToStorage(List<InventoryItemSO> items)
    {
        foreach (InventoryItemSO item in items)
        {
            if (storedItems.ContainsKey(item))
            {
                storedItems[item] += 1;
            }
            else
            {
                storedItems.Add(item, 1);
            }
        }
        onChangedStoredItems?.Invoke(storedItems);
    }

    public void SubtractItemCost(InventoryItemSO item, int cost)
    {
        if (storedItems.ContainsKey(item))
        {
            storedItems[item] -= cost;
        }
        onChangedStoredItems?.Invoke(storedItems);
    }

    public int GetItemAmount(InventoryItemSO item)
    {
        if (storedItems.ContainsKey(item))
        {
            return storedItems[item];
        }
        return -1;
    }
}
