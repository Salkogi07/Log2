using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSound : MonoBehaviour
{
    public AudioClip clip1;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LevelPlaySound()
    {
        audioSource.clip = clip1;
        audioSource.Play();
    }
}
