using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerSkill skill;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pos;

    public float playerAttack_coolTime;
    public float playerAttack_Time;

    private void Awake()
    {
        skill = GetComponent<PlayerSkill>();
    }

    private void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        if (skill.isSkill3)
            playerAttack_coolTime = GameManager.instance.skill3_playerAttackCooldown;
        else
            playerAttack_coolTime = GameManager.instance.playerAttack_cooldown;

        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos.transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        pos.transform.rotation = Quaternion.Euler(0, 0, z);

        if(playerAttack_Time <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet,pos.transform.position,pos.transform.rotation);
                playerAttack_Time = playerAttack_coolTime;
            }
        }
        playerAttack_Time -= Time.deltaTime;
    }
}
