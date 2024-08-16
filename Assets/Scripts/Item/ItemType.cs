using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public GameManager.InfoType type;
    private SpriteRenderer sprite;
    public AudioClip pickupSound; // �������� ���� �� ����� �Ҹ�
    private AudioSource audioSource; // ����� �ҽ��� ������ ����
    private Collider2D coll;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.UseItem((int)type);

            audioSource.PlayOneShot(pickupSound);

            sprite.enabled = false;
            coll.enabled = false;
            Destroy(gameObject, pickupSound.length);
        }
    }
}
