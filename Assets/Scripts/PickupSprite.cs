using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private SpriteRenderer backSprite;

    public void SetSprites(InventoryItemSO item)
    {
        sprite.sprite = item.sprite;
        backSprite.sprite = item.sprite;
    }
}
