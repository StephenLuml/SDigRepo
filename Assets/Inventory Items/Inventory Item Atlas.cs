using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "new I Item Atlas", menuName = "Inv Item Atlas")]
public class InventoryItemAtlas : ScriptableObject
{
    public List<InventoryItemSO> inventoryItems = new List<InventoryItemSO>();
}
