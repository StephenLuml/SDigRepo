using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip upgradeClink;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerController.onSuccessfulUpgrade += PlayUpgradeSound;
    }
    private void OnDisable()
    {
        PlayerController.onSuccessfulUpgrade -= PlayUpgradeSound;
    }

    private void PlayUpgradeSound(PlayerUpgrade upgrade)
    {
        audioSource.PlayOneShot(upgradeClink);
    }
}
