using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMapTexture : MonoBehaviour
{
    [SerializeField]
    private GameObject mapTextureObject;
    [SerializeField]
    private string mapName;

    private void Start()
    {
        if(mapTextureObject != null)
        {
            mapTextureObject.GetComponent<RawImage>().texture = LoadMapFromPNG();
        }
    }

    private Texture2D LoadMapFromPNG()
    {
        if (!File.Exists(GetMapFilePath()))
        {
            return new Texture2D(2, 2, TextureFormat.RGB24, false);
        }
        byte[] bytes = File.ReadAllBytes(GetMapFilePath());
        Texture2D mapTex = new Texture2D(2, 2, TextureFormat.RGB24, false);
        mapTex.LoadImage(bytes);
        return mapTex;
    }

    private string GetMapFilePath()
    {
        return MapDataPath.MapPath(mapName);
    }
}
