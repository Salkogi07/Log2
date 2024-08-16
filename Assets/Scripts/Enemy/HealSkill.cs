using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(5, 5);
    public LayerMask enemyLayer;
    public int healAmount = 10;
    public float Skill_Interval = 10f;

    public GameObject heallObj;
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        StartCoroutine(AutoSkill());
    }

    IEnumerator AutoSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(Skill_Interval);
            enemy.isSkillMove = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine(HealWithPause());
        }
    }

    IEnumerator HealWithPause()
    {
        heallObj.SetActive(true);
        enemy.isSkillMove = false;
        yield return new WaitForSeconds(1f);

        HealEnemies();
        heallObj.SetActive(false);
        enemy.isSkillMove = true;
    }

    void HealEnemies()
    {
        Vector2 center = transform.position;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(center, boxSize, 0f, enemyLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Heal the enemy by 10 but don't exceed maxHealth
                    enemy.health = Mathf.Min(enemy.health + healAmount, enemy.maxHealth);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
