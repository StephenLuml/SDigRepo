using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateNewMaps : MonoBehaviour
{
    //public MapToLoadSO smallMapLoader;
    public TerrainGeneration smallMapGenerator;

    //public MapToLoadSO map2Loader;
    public TerrainGeneration map2Generator;

    //public MapToLoadSO map3Loader;
    public TerrainGeneration map3Generator;

    private void Start()
    {
        smallMapGenerator.CreateWorldPng();
        map2Generator.CreateWorldPng();
        map3Generator.CreateWorldPng();
    }
}
