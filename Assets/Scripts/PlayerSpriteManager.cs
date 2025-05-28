using System;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cartSprites;
    [SerializeField]
    private GameObject drillSprite;
    [SerializeField]
    private DrillSpriteLocation[] drillSpriteLocations;

    [SerializeField]
    private GameObject pickupAnimationFab;

    private void OnEnable()
    {
        PlayerInventory.OnPickUpItem += PickUpAnimation;
    }
    private void OnDisable()
    {
        PlayerInventory.OnPickUpItem -= PickUpAnimation;
    }

    public void SetSpriteDirection(Vector2 direction)
    {
        if (direction.x == 1)
        {
            FaceRight();
        }
        if (direction.x == -1)
        {
            FaceLeft();
        }
        if (areDrillSpritesSet())
        {
            if (direction.y == 1)
            {
                PointDrillUp();
            }
            else if (direction.y == -1)
            {
                PointDrillDown();
            }
            else
            {
                PointDrillForward();
            }
        }
    }

    public void PickUpAnimation(InventoryItemSO item)
    {
        GameObject newPickUp = Instantiate(pickupAnimationFab, cartSprites.transform);
        newPickUp.GetComponent<PickupSprite>().SetSprites(item);
    }

    private void PointDrillForward()
    {
        SetDrillSprite(drillSpriteLocations[0]);
    }

    private void PointDrillDown()
    {
        SetDrillSprite(drillSpriteLocations[2]);
    }

    private void PointDrillUp()
    {
        SetDrillSprite(drillSpriteLocations[1]);
    }
    
    private bool areDrillSpritesSet()
    {
        return drillSpriteLocations != null && drillSpriteLocations.Length > 1;
    }

    private void FaceLeft()
    {
        cartSprites.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FaceRight()
    {
        cartSprites.transform.localScale = new Vector3(1, 1, 1);
    }

    private void SetDrillSprite(DrillSpriteLocation location)
    {
        drillSprite.transform.localPosition = location.Position;
        drillSprite.transform.localRotation = Quaternion.Euler(location.Rotation);
    }
}

[Serializable]
public class DrillSpriteLocation
{
    public Vector3 Position;
    public Vector3 Rotation;
}