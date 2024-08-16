using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public GameManager.InfoType type;
    public AudioClip pickupSound; // �������� ���� �� ����� �Ҹ�
    private AudioSource audioSource; // ����� �ҽ��� ������ ����

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
