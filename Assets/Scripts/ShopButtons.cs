using System;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public static Action<UpgradeType> onPurchaseUpgrade;
    public static Action onCloseShopUI;

    public void PurchaseDrillUpgrade()
    {
        onPurchaseUpgrade?.Invoke(UpgradeType.Drill);
    }
    public void PurchaseEnergyUpgrade()
    {
        onPurchaseUpgrade?.Invoke(UpgradeType.Energy);
    }
    public void PurchaseSpeedUpgrade()
    {
        onPurchaseUpgrade?.Invoke(UpgradeType.Speed);
    }
    public void CloseShopUI()
    {
        onCloseShopUI?.Invoke();
    }
}
