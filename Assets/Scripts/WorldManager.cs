using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public int worldHeight { get; private set; } = 0;
    public int worldWidth { get; private set; } = 0;
    [SerializeField]
    private int chunkHeight = 16;
    //[SerializeField]
    //private int chunkWidth = 16;
    [SerializeField]
    private float cullDistance = 25;

    private GameObject[] worldChunks;
    public TGrid<GameObject> mapGrid { get; private set; }
    public TGrid<GameObject> shopGrid { get; private set; }

    [SerializeField]
    private GameObject tilePreFab;

    [SerializeField]
    private TileAtlas tileAtlas;

    [SerializeField]
    private GameObject player;

    public static Action<Vector2Int> onBuiltWorld;

    //For Debug
    private Dictionary<Color32, string> seenColors = new Dictionary<Color32, string>();

    private void OnEnable()
    {
        Tile.onTileDug += RemoveTile;
        BaseShop.onShopPosition += AddShopLocation;
    }
    private void OnDisable()
    {
        Tile.onTileDug -= RemoveTile;
        BaseShop.onShopPosition -= AddShopLocation;
    }

    public void Init(Texture2D mapTexture)
    {
        worldWidth = mapTexture.width;
        worldHeight = mapTexture.height;
        CreateChunks();
        mapGrid = new TGrid<GameObject>(worldWidth, worldHeight);
        shopGrid = new TGrid<GameObject>(worldWidth, worldHeight+1);

        GenerateTilesFromPNG(mapTexture);
        onBuiltWorld?.Invoke(new Vector2Int(worldWidth, worldHeight));
    }

    private void GenerateTilesFromPNG(Texture2D mapTexture)
    {
        for (int x = 0; x < worldHeight; x++)
        {
            for (int y = 0; y < worldWidth; y++)
            {
                Color32 pixCol = mapTexture.GetPixel(x, y);
                TileSO tile = tileAtlas.GetTileByColor(pixCol);
                if (!seenColors.ContainsKey(pixCol))
                {
                    seenColors.Add(pixCol, tile.name);
                }
                if(tile == tileAtlas.backstone)
                {
                    PlaceTile(x, y, tile, false);
                }
                else
                {
                    PlaceTile(x, y, tile);
                }
            }
        }
    }

    private void CreateChunks()
    {
        int hChunks = Mathf.CeilToInt(worldHeight / chunkHeight) + 1;
        worldChunks = new GameObject[hChunks];

        for (int i = 0; i < worldChunks.Length; i++)
        {
            GameObject newChunk = new GameObject(name = $"Chunk {i}");
            newChunk.transform.parent = transform;
            newChunk.transform.position = new Vector3(0, (float)i / (hChunks - 1) * worldHeight);
            worldChunks[i] = newChunk;
        }
    }

    private void PlaceTile(int x, int y, TileSO tile, bool isForeGround = true)
    {
        GameObject newTile = Instantiate(tilePreFab);
        Tile newTileData = newTile.GetComponent<Tile>();
        newTileData.Init(tile, isForeGround);

        int chunkCoord = Mathf.FloorToInt(y / chunkHeight);
        newTile.transform.parent = worldChunks[chunkCoord].transform;
        newTile.transform.position = new Vector2(x, y);

        if (isForeGround)
        {
            mapGrid.SetCell(new Vector2Int(x, y), newTile);
        }
    }

    public void RemoveTile(int x, int y)
    {
        mapGrid.SetCell(new Vector2Int(x, y), null);
    }
    public void RemoveTile(Vector2Int tilePos)
    {
        RemoveTile(tilePos.x, tilePos.y);
    }

    public GridBlockType GetTileTypeAt(Vector2Int position)
    {
        if (mapGrid.GetCell(position))
        {
            return GridBlockType.Block;
        }
        if (shopGrid.GetCell(position))
        {
            return GridBlockType.Shop;
        }
        return GridBlockType.Empty;
    }

    private void AddShopLocation(Vector2Int location, GameObject shop)
    {
        shopGrid.SetCell(location, shop);
    }

    private void CullDistantChunks()
    {
        foreach (var chunk in worldChunks)
        {
            if (DistanceToPlayer(chunk.transform.position) > cullDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }

    private float DistanceToPlayer(Vector3 pos)
    {
        if (!player)
        {
            return 0f;
        }
        return Mathf.Abs(player.transform.position.y - pos.y);
    }

    public void EnterShopAt(Vector2Int pos)
    {
        shopGrid.GetCell(pos).GetComponent<BaseShop>().EnterShop();
    }
}
