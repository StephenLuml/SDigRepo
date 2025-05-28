using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField]
    private int startingX = 0;

    [SerializeField]
    private bool reposition = true;

    private void OnEnable()
    {
        WorldManager.onBuiltWorld += Position;
    }
    private void OnDisable()
    {
        WorldManager.onBuiltWorld -= Position;
    }

    private void Position(Vector2Int worldSize)
    {
        if (reposition)
        {
            transform.position = new Vector3(startingX, worldSize.y, transform.position.z);
        }
    }
}
