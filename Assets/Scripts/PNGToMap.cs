using System.IO;
using UnityEngine;

public class PNGToMap : MonoBehaviour
{
    [SerializeField]
    private MapToLoadSO mapSO;
    [SerializeField]
    private WorldManager worldManager;
    [SerializeField]
    private TerrainGeneration generator;

    private void Start()
    {
        string filePath = Application.dataPath + "/" + mapSO.mapName + ".png";
        if (File.Exists(filePath))
        {
            LoadMapFromPNG(filePath);
        }
        else
        {
            generator.CreateWorldPng();
            LoadMapFromPNG(filePath);
        }
    }

    private void LoadMapFromPNG(string mapPath)
    {
        byte[] bytes = File.ReadAllBytes(mapPath);
        Texture2D mapTex = new Texture2D(2, 2, TextureFormat.RGB24, false);
        mapTex.LoadImage(bytes);
        worldManager.Init(mapTex);
    }
}
