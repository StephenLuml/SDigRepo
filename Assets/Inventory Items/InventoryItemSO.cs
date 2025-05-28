using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new I Item", menuName ="Inv Item")]
public class InventoryItemSO : ScriptableObject
{
    new public string name;
    public int index = 0;
    public int size = 1;
    public Sprite sprite;

    public InventoryItemSO(int size)
    {
        CreateInstance<InventoryItemSO>().size = size;
    }
}
