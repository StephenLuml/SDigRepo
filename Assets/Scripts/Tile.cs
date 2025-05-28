using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite dropSprite;
    public int durability = 0;
    public bool drops = false;

    private int maxDurability;

    public Sprite breakingSprite;
    private SpriteRenderer breakingOverlay;
    public InventoryItemSO dropItem;

    public static Action<InventoryItemSO> onItemDropped;
    public static Action<Vector2Int> onTileDug;

    private void Start()
    {
        maxDurability = durability;
    }

    public void Init(TileSO tile, bool isForeGround = true)
    {
        SpriteRenderer newRenderer = GetComponent<SpriteRenderer>();
        newRenderer.sprite = tile.sprite;
        if (!isForeGround)
        {
            newRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }

        if (tile.dropItem)
        {
            dropItem = tile.dropItem;
            drops = true;
        }

        durability = tile.durability;
        name = tile.name;
    }

    public void Dig(int strength)
    {
        durability -= strength;

        if (!breakingOverlay)
        {
            CreateOverlay();
        }

        if (durability <= 0)
        {
            if (drops)
            {
                DropItem();
            }
            Vector2 pos = gameObject.transform.position;
            //TerrainGeneration.Instance.RemoveTile((int)pos.x, (int)pos.y);
            onTileDug?.Invoke(new Vector2Int((int)pos.x, (int)pos.y));
            RemoveOverlay();
            Fadeout();
        }
        else
        {
            IncreaseOverlayOpacity();
        }
    }

    private void Fadeout()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(0.5f, 0.5f, 0.5f);
    }

    private void RemoveOverlay()
    {
        Destroy(breakingOverlay.gameObject);
    }

    private void IncreaseOverlayOpacity()
    {
        Color color = breakingOverlay.color;
        float brokenness = 1 - (durability / (float)maxDurability);
        color.a = brokenness;
        breakingOverlay.color = color;
    }

    private void CreateOverlay()
    {
        GameObject overlay = new GameObject();
        overlay.transform.SetParent(transform, false);
        overlay.transform.localScale = Vector3.one * 2;
        breakingOverlay = overlay.AddComponent<SpriteRenderer>();
        breakingOverlay.sortingOrder = 1;
        breakingOverlay.sprite = breakingSprite;
    }

    private void DropItem()
    {
        if (dropItem)
        {
            onItemDropped?.Invoke(dropItem);
        }
    }
}
