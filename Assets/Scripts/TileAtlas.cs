using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newTileAtlas", menuName ="TileAtlas")]
public class TileAtlas : ScriptableObject
{
    [SerializeField]
    private TileData[] tileDatas;
    public TileSO plant;
    public TileSO grass;
    public TileSO dirt;
    public TileSO backstone;
    public TileSO stone;
    public TileSO coal;
    public TileSO ironore;
    public TileSO diamond;

    private Dictionary<Color32, TileSO> colDict;

    public TileSO GetTileByColor(Color32 getCol)
    {
        if (colDict is null || colDict.Count < 1)
        {
            colDict = new Dictionary<Color32, TileSO>
            {
                { plant.mapColor, plant },
                { grass.mapColor, grass },
                { dirt.mapColor, dirt },
                { backstone.mapColor, backstone },
                { stone.mapColor, stone },
                { coal.mapColor, coal },
                { ironore.mapColor, ironore },
                { diamond.mapColor, diamond },
            };
        }
        if (colDict.ContainsKey(getCol))
        {
            return colDict[getCol];
        }
        return backstone;
    }
}

[Serializable]
public class TileData
{
    public string name = "";
    public TileSO tile;
}