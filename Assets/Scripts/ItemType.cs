using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public GameManager.InfoType type;
    public AudioClip pickupSound; // 아이템을 먹을 때 재생할 소리
    private AudioSource audioSource; // 오디오 소스를 저장할 변수

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.UseItem((int)type);

            audioSource.PlayOneShot(pickupSound);

            Destroy(gameObject, pickupSound.length);
        }
    }
}
