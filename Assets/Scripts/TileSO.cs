using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newTileObject", menuName ="Tile SO")]
public class TileSO : ScriptableObject
{
    public string tileName;
    public Sprite sprite;
    public int durability = 1;
    public bool drops = true;
    public InventoryItemSO dropItem;
    public Color mapColor;
}
