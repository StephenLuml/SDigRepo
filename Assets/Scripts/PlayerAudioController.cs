using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip drillClip;
    [SerializeField]
    private float[] drillClipTiming = { 0, 0 };

    private float timer = 0;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                StopAudio();
            }
        }
    }

    public void PlayDrillAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(drillClip);
            audioSource.time = drillClipTiming[0];
            timer = drillClipTiming[1];
        }
    }
    private void StopAudio()
    {
        audioSource.Stop();
    }
}
