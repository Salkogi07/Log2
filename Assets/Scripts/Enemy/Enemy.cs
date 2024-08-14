using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float getExp;
    public Rigidbody2D target;

    bool isLive;
    public bool isSkillMove = true;

    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer sprite;
    Animator anim;
    WaitForFixedUpdate wait;
    public GameObject hitPartical;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.IsLive)
            return;

        if(!isLive || GameManager.instance.isEnemyMove || !isSkillMove)
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
        if (GameManager.instance.IsLive)
            return;

        sprite.flipX = target.position.x < rb.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rb.simulated = true;
        sprite.sortingOrder = 2;
        health = maxHealth;
    }


}
