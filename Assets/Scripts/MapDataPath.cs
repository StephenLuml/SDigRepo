using UnityEngine;

public class MapDataPath : MonoBehaviour
{
    public static string MapPath(string mapName)
    {
        //return Application.persistentDataPath + "/" + mapName + ".png";
        return Application.dataPath + "/" + mapName + ".png";
    }
}
