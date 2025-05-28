using UnityEngine;

[CreateAssetMenu(fileName ="newOreGenObject", menuName ="Ore Gen SO")]
public class OreGenSO : ScriptableObject
{
    public string oreName = string.Empty;
    public bool genToggle = true;
    public int value;
    public int minHeight = 0;
    public int maxHeight = 100;
    public float noiseFreq;
    public float noiseThreshold;
    public int seed;
    public Sprite sprite;
    public Color mapColor;
    public TileSO tile;
}