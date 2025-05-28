using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI currentHeldText;
    public TextMeshProUGUI maxHeldText;

    public void Init(int maxHeld)
    {
        SetHeld(0);
        SetMaxHeld(maxHeld);
    }

    public void SetHeld(int amt)
    {
        currentHeldText.text = amt.ToString();
    }

    public void SetMaxHeld(int amt)
    {
        maxHeldText.text = amt.ToString();
    }
}
