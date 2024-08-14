using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject skill3_partical;

    [SerializeField] private float skill3_Duration = 3f;
    [SerializeField] private int skill4_HealAmount = 10;
    [SerializeField] private float skill2_teleporDistance = 3f;

    public bool isSkill3;

    public void UseSkill1()
    {
        if(GameManager.instance.mana >= GameManager.instance.skill1_UseMana)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            Instantiate(damagePrefab, (Vector2)transform.position + direction, Quaternion.identity);
            GameManager.instance.AddMana(-GameManager.instance.skill1_UseMana);
        }
    }

    public void UseSkill2()
    {
        if(GameManager.instance.mana >= GameManager.instance.skill2_UseMana)
        {
            Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 direction = len.normalized;
            transform.position += (Vector3)(direction * skill2_teleporDistance);
            GameManager.instance.AddMana(-GameManager.instance.skill2_UseMana);
        }
    }

    public void UseSkill3()
    {
        if (!isSkill3)
        {
            if(GameManager.instance.mana >= GameManager.instance.skill3_UseMana)
            {
                StartCoroutine(Skill3_Start());
                GameManager.instance.AddMana(-GameManager.instance.skill3_UseMana);
            }
        }
    }

    IEnumerator Skill3_Start()
    {
        isSkill3 = true;
        skill3_partical.SetActive(true);
        yield return new WaitForSeconds(skill3_Duration);
        isSkill3 = false;
        skill3_partical.SetActive(false);
    }

    public void UseSkill4()
    {
        if(GameManager.instance.mana >= GameManager.instance.skill4_UseMana)
        {
            GameManager.instance.AddHp(skill4_HealAmount);
            GameManager.instance.AddMana(-GameManager.instance.skill4_UseMana);
        }
    }
}
