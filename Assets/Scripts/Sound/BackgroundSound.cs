using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NoBossSound()
    {
        audioSource.clip = clip1;
        audioSource.Play();
    }

    public void BossSound()
    {
        audioSource.clip = clip2;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
