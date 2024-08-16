using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttack : MonoBehaviour
{
    private Enemy enemy;
    public float Skill_Interval = 5f;

    void Awake()
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
            StopPlayerSkill();
        }
    }

    void StopPlayerSkill()
    {
        GameManager.instance.Use_SkillStopEnemy();
        enemy.isSkillMove = true;
    }
}
