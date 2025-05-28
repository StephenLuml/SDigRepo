using System;
using UnityEngine;

public class BaseShop : MonoBehaviour
{
    [SerializeField]
    private int startingX = 0;
    [SerializeField]
    private bool reposition = true;

    [SerializeField]
    private GameObject shopObject;
    public GameObject shopCanvas { get; private set; }

    public static Action<BaseShop> onEnterShop;
    public static Action<BaseShop> onExitShop;

    public static Action<Vector2Int, GameObject> onShopPosition;

    private void OnEnable()
    {
        WorldManager.onBuiltWorld += PositionToWorldHeight;
    }
    private void OnDisable()
    {
        WorldManager.onBuiltWorld -= PositionToWorldHeight;
    }

    private void Start()
    {
        shopCanvas = shopObject;
    }

    private void PositionToWorldHeight(Vector2Int worldSize)
    {
        if (reposition)
        {
            transform.position = new Vector3(startingX, worldSize.y, transform.position.z);
            onShopPosition?.Invoke(new Vector2Int(startingX, worldSize.y), gameObject);
        }
    }

    public void EnterShop()
    {
        onEnterShop?.Invoke(this);
    }

    public void ExitShop()
    {
        onExitShop?.Invoke(this);
    }
}
