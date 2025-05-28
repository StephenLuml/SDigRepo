using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnFailedUpgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToSpawn;

    private void OnEnable()
    {
        PlayerController.onFailedUpgrade += FailedUpgrade;
    }
    private void OnDisable()
    {
        PlayerController.onFailedUpgrade -= FailedUpgrade;
    }

    private void FailedUpgrade(PlayerUpgrade upgrade)
    {
        GameObject newObject = Instantiate(ObjectToSpawn);
        Vector3 spawnPos = Input.mousePosition;
        spawnPos.z = -1;
        newObject.transform.position = spawnPos;
        newObject.transform.SetParent(transform, true);
    }
}
