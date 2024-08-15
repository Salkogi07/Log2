using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDamage : MonoBehaviour
{
    private enum InfoType { Attack, Skill}
    [SerializeField] private InfoType type;

    [SerializeField] public float damage;
    [SerializeField] private float duringObject;

    Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        switch (type)
        {
            case InfoType.Attack:
                damage = GameManager.instance.playerAttack_damage;
                tr.localScale = new Vector2(GameManager.instance.playerBullet_scale,GameManager.instance.playerBullet_scale);
                break;
        }
    }

    private void Start()
    {
        Destroy(gameObject,duringObject);
    }
}
