using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public int getExp;
    public Rigidbody2D target;

    public bool isSkillMove = true;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Collider2D coll;
    WaitForFixedUpdate wait;
    public GameObject hitPartical;
    public GameObject diePartical;

    public AudioClip Sound; // 아이템을 먹을 때 재생할 소리
    private AudioSource audioSource; // 오디오 소스를 저장할 변수

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        if(!GameManager.instance.isEnemyMove || !isSkillMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        if (!GameManager.instance.isEnemyMove)
            return;

        sprite.flipX = target.position.x < rb.position.x;
    }

    private void Start()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<PlayerAttackDamage>().damage;
        Destroy(collision.gameObject);
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            audioSource.PlayOneShot(Sound);
            Instantiate(hitPartical,gameObject.transform.position,Quaternion.identity);
        }
        else
        {
            if (gameObject.CompareTag("Boss"))
            {
                GameManager.instance.GameWin();
                audioSource.PlayOneShot(Sound);
                sprite.enabled = false;
                coll.enabled = false;
                Instantiate(diePartical,gameObject.transform.position,Quaternion.identity);
                Destroy(gameObject,Sound.length);
            }
            else
            {
                GameManager.instance.killEnemyCount++;
                GameManager.instance.AddExp(getExp);
                audioSource.PlayOneShot(Sound);
                sprite.enabled = false;
                coll.enabled = false;
                Instantiate(diePartical,gameObject.transform.position,Quaternion.identity);
                Destroy(gameObject,Sound.length);
            }
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
}
