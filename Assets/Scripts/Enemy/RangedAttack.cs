using System;
using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
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
            enemy.isSkillMove = false;
            yield return new WaitForSeconds(0.5f);
            FireBullet();
        }
    }

    void FireBullet()
    {
        Vector3 directionToPlayer = target.position - transform.position;
        Quaternion lookRotation = Quaternion.AngleAxis(Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg, Vector3.forward);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, lookRotation);
        enemy.isSkillMove = true;
    }
}
