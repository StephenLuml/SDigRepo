using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMapToLoadSO",menuName ="MapToLoadSO")]
public class MapToLoadSO : ScriptableObject
{
    public string mapName = "";

    public void SetMap(string mapName)
    {
        this.mapName = mapName;
    }
}
