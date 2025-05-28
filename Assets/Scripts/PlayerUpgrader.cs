using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUpgrader : MonoBehaviour
{
    [SerializeField]
    private List<PlayerUpgrade> upgrades = new List<PlayerUpgrade>();

    private PlayerController playerController;

    private void OnEnable()
    {
        ShopButtons.onPurchaseUpgrade += UpgradePlayer;
    }
    private void OnDisable()
    {
        ShopButtons.onPurchaseUpgrade -= UpgradePlayer;
    }

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void UpgradePlayer(UpgradeType upgradeType)
    {
        foreach (PlayerUpgrade upgrade in upgrades)
        {
            if(upgradeType == upgrade.UpgradeTarget)
            {
                if (HaveResources(upgrade))
                {
                    StoredItems.Instance.SubtractItemCost(upgrade.ItemNeeded, upgrade.Cost);
                    upgrade.ActionToTake?.Invoke();
                    playerController.SuccessfulUpgrade(upgrade);
                }
                else
                {
                    //Missing resources
                    playerController.FailedUpgrade(upgrade);
                }
                break;
            }
        }
    }

    public void UpgradeDrill()
    {
        playerController.UpgradeDrill();
    }
    public void UpgradeEnergy()
    {
        playerController.UpgradeEnergy();
    }
    public void UpgradeSpeed()
    {
        playerController.UpgradeSpeed();
    }

    private bool HaveResources(PlayerUpgrade upgradeToCheck)
    {
        int amtStored = StoredItems.Instance.GetItemAmount(upgradeToCheck.ItemNeeded);
        return amtStored >= upgradeToCheck.Cost;
    }
}

[Serializable]
public class PlayerUpgrade
{
    public UpgradeType UpgradeTarget;
    public InventoryItemSO ItemNeeded;
    public int Cost = 1;
    public UnityEvent ActionToTake;
}
