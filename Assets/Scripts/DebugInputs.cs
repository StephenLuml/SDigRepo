using UnityEngine;

public class DebugInputs : MonoBehaviour
{
    public PlayerInventory pInv;
    public InventoryItemSO testItem;
    public PlayerController controller;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            pInv.AddToBag(testItem, 1);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            controller?.UpgradeReset();
        }
    }
}
