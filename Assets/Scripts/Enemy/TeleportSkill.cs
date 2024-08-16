using System;
using System.Collections;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    private Transform target;
    Enemy enemy;

    public float fireInterval = 5f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        StartCoroutine(AutoFire());
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);

            while (!(Vector3.Distance(target.position, transform.position) < 10))
            {
                yield return null;
            }

            enemy.isSkillMove = false;
            yield return new WaitForSeconds(0.5f);
            Teleport();
        }
    }

    void Teleport()
    {
        transform.position = target.position + (target.position - transform.position).normalized;
        enemy.isSkillMove = true;
    }
}
