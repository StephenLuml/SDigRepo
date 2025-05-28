using System.Collections.Generic;
using UnityEngine;

public class ShopCanvasMan : MonoBehaviour
{
    [SerializeField]
    private Dictionary<BaseShop, GameObject> canvases = new Dictionary<BaseShop, GameObject>();

    private void OnEnable()
    {
        PlayerController.onExitShop += HideCanvas;
        BaseShop.onEnterShop += ShowCanvas;
    }
    private void OnDisable()
    {
        PlayerController.onExitShop -= HideCanvas;
        BaseShop.onEnterShop -= ShowCanvas;
    }

    private void HideCanvas()
    {
        foreach (var shop in canvases.Values)
        {
            shop.SetActive(false);
        }
    }

    private void ShowCanvas(BaseShop shopToOpen)
    {
        if (canvases.ContainsKey(shopToOpen))
        {
            canvases[shopToOpen].SetActive(true);
        }
        else
        {
            GameObject newCanvas = Instantiate(shopToOpen.shopCanvas, transform);
            canvases.Add(shopToOpen, newCanvas);
            canvases[shopToOpen].SetActive(true);
        }
    }
}
