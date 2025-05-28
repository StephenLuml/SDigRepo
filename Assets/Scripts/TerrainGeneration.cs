using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TerrainGeneration : MonoBehaviour
{
    [Header("World")]
    public int worldHeight = 50;
    public int worldWidth = 50;
    public int chunkHeight = 16;
    public int chunkWidth = 16;

    [Header("Layers")]
    public int topLayerHeight = 7;

    [Header("Generation")]
    public float seed = 0;
    public float caveNoiseFreq = 0.025f;
    public float generateTileThreshold = 1;
    //public float plantChance = 0.1f;
    
    [Header("Ores")]
    public OreGenSO[] Ores;

    [Header("Sprites")]
    public TileAtlas tileAtlas;

    public Texture2D caveNoiseTexture;
    [SerializeField]
    private bool useMapColor = true;
    public Texture2D[] oreNoiseTextures;
    public Texture2D doneTexture;

    public GameObject tilePreFab;

    [SerializeField]
    private GameObject mapTextureObject;

    [SerializeField]
    private MapToLoadSO mapLoadObject;

    private void OnValidate()
    {
        CreateTextures();
    }

    public void CreateTextures()
    {
        caveNoiseTexture = GenerateCaveNoise();

        oreNoiseTextures = new Texture2D[Ores.Length];
        for (int i = 0; i < Ores.Length; i++)
        {
            oreNoiseTextures[i] = GenerateOreTexture(Ores[i]);
        }
    }

    public void CreateWorldPng()
    {
        CreateTextures();
        doneTexture = new Texture2D(worldWidth, worldHeight, TextureFormat.RGB24, false);
        string filePath = Application.dataPath + "/" + mapLoadObject.mapName + ".png";

        for (int x = 0; x < worldHeight; x++)
        {
            for (int y = 0; y < worldWidth; y++)
            {
                float tileNoise = caveNoiseTexture.GetPixel(x,y).r;
                Color pixelCol = GetPixColByHeight(y);
                if (pixelCol == tileAtlas.stone.mapColor)
                {
                    for (int i = 0; i < Ores.Length; i++)
                    {
                        if (!Ores[i].genToggle)
                            continue;

                        Color checkColor = useMapColor ? Ores[i].mapColor : Color.black;
                        if (oreNoiseTextures[i].GetPixel(x,y) == checkColor)
                        {
                            pixelCol = Ores[i].tile.mapColor;
                        }
                    }
                }
                if (tileNoise < generateTileThreshold)
                {
                    pixelCol = tileAtlas.backstone.mapColor;
                }
                doneTexture.SetPixel(x,y,pixelCol);
            }
        }
        doneTexture.Apply();
        SaveTexture(doneTexture, filePath);
        if(mapTextureObject != null)
        {
            mapTextureObject.GetComponent<RawImage>().texture = doneTexture;
        }
    }

    private void SaveTexture(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    private Color GetPixColByHeight(int y)
    {
        Color mapColor = tileAtlas.stone.mapColor;
        if (y >= worldHeight - 1)
        {
            mapColor = tileAtlas.grass.mapColor;
        }
        else if (y > worldHeight - topLayerHeight)
        {
            mapColor = tileAtlas.dirt.mapColor;
        }

        return mapColor;
    }

    private Texture2D GenerateCaveNoise()
    {
        Texture2D newTexture = new Texture2D(worldWidth, worldHeight);

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                float noise = Get2DNoise(x, y, caveNoiseFreq);
                newTexture.SetPixel(x, y, new Color(noise, noise, noise));
            }
        }

        newTexture.Apply();
        return newTexture;
    }

    private Texture2D GenerateOreTexture(OreGenSO ore)
    {
        Texture2D newTexture = new Texture2D(worldWidth, worldHeight);
        int minHeight = (int)(ore.minHeight / 100f * newTexture.height);
        int maxHeight = (int)(ore.maxHeight / 100f * newTexture.height);

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = minHeight; y < maxHeight; y++)
            {
                float noise = Get2DNoise(x, y, ore.noiseFreq, ore.seed);
                if (noise > ore.noiseThreshold)
                {
                    Color pixelColor = Color.black;
                    if (useMapColor)
                    {
                        pixelColor = ore.mapColor;
                    }
                    newTexture.SetPixel(x, y, pixelColor);
                }
            }
        }

        newTexture.Apply();
        return newTexture;
    }

    private float Get2DNoise(int x, int y, float noiseFrequency, int addedSeed)
    {
        float xNoise = (x + seed + addedSeed) * noiseFrequency;
        float yNoise = (y + seed + addedSeed) * noiseFrequency;

        float noise = Mathf.PerlinNoise(xNoise, yNoise);

        return noise;
    }
    private float Get2DNoise(int x, int y, float noiseFrequency)
    {
        return Get2DNoise(x, y, noiseFrequency, 0);
    }
}
