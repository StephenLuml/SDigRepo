using System.Collections.Generic;
using UnityEngine;
public class TGrid<T>
{
    private int width, height;
    private Dictionary<Vector2Int, T> gridData;

    public TGrid(int width, int height)
    {
        gridData = new Dictionary<Vector2Int, T>();
        this.width = width;
        this.height = height;
    }

    public void SetCell(Vector2Int position, T value)
    {
        gridData[position] = value;
    }

    public T GetCell(Vector2Int position)
    {
        return gridData.TryGetValue(position, out T value) ? value : default;
    }

    public bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.y >= 0 && position.x < width && position.y < height;
    }
}
