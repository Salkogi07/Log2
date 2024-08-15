using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;
    PlayerSkill skill;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        skill = GetComponent<PlayerSkill>();
    }

    void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skill.UseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skill.UseSkill2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skill.UseSkill3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skill.UseSkill4();
        }

        speed = GameManager.instance.speed;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        anim.SetFloat("Speed",inputVec.magnitude);

        if(inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.IsLive || !GameManager.instance.isDamge || !GameManager.instance.isMasterDamge)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.IsLive || !GameManager.instance.isDamge || !GameManager.instance.isMasterDamge)
            return;


        if (collision.CompareTag("EnemyBullet"))
        {
            if (GameManager.instance.health < 0)
            {
                for (int index = 2; index < transform.childCount; index++)
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }
            }
            GameManager.instance.GameOver();
        }
    }
}
