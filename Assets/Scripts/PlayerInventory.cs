using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int maxCapacity = 10;

    [SerializeField]
    private InventoryUI inventoryUI;

    private List<InventoryItemSO> heldItems;

    public static Action<InventoryItemSO> OnPickUpItem;
    public static Action<List<InventoryItemSO>> onCollectedItems;

    private void OnEnable()
    {
        Tile.onItemDropped += PickUpDrop;
        PlayerController.onSurface += DumpInventory;
    }
    private void OnDisable()
    {
        Tile.onItemDropped -= PickUpDrop;
        PlayerController.onSurface -= DumpInventory;
    }

    private void Start()
    {
        heldItems = new List<InventoryItemSO>();

        if (inventoryUI)
            inventoryUI.Init(maxCapacity);
        else
            Debug.Log("No inventory UI set.");
    }

    public void PickUpDrop(InventoryItemSO addItem)
    {
        AddToBag(addItem, 1);
    }
    public bool AddToBag(InventoryItemSO addItem, int amt)
    {
        bool fitAllItems = true;
        int capacity = maxCapacity - GetBagItems();

        if (capacity > amt)
        {
            fitAllItems = false;
        }

        int count = 0;
        while (heldItems.Count < maxCapacity && count < amt)
        {
            heldItems.Add(addItem);
            OnPickUpItem?.Invoke(addItem);
            count++;
        }
        inventoryUI.SetHeld(GetBagItems());

        return fitAllItems;
    }

    private int GetBagItems()
    {
        int count = 0;
        foreach (InventoryItemSO item in heldItems)
        {
            count++;
        }
        return count;
    }

    public void DumpInventory()
    {
        onCollectedItems?.Invoke(heldItems);
        heldItems.Clear();
        inventoryUI.SetHeld(GetBagItems());
    }
}
