using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum InfoType { Exp, Speed, Shield, Heal, Stop }

    [Header("# Game Control")]
    public bool IsLive = false;
    [SerializeField] public float gameTime;
    [SerializeField] public float maxGameTime = 120f;
    [Space(20)]

    [Header("Player info")]
    public float health;
    public float maxHealth = 100;
    public float mana;
    public int maxMana = 50;
    [Space(10)]

    public int killEnemyCount;
    [Space(10)]

    public int playerLevel;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 50, 70, 100, 150, 200, 400, 500, 600, 700, 800, 900 };
    [Space(10)]

    public float speed;
    public int speedCount = 0;
    [Space(10)]

    public bool isDamge = true;
    public bool isMasterDamge = true;
    [Space(20)]

    [Header("Player Skill info")]
    public int skill1_UseMana = 30;
    public int skill2_UseMana = 20;
    public float skill3_playerAttackCooldown = 0.2f;
    public int skill3_UseMana = 20;
    public int skill4_UseMana = 30;
    [Space(20)]

    [Header("Player Ability")]
    public int playerAttack_damage = 11;
    public float playerAttack_cooldown = 0.8f;
    public float playerBullet_scale = 5f;
    [Space(10)]

    public int playerBulletSize_UpgradeLevel = 0;
    public int playerBulletSize_UpgradeLevelMax = 5;
    public int playerAttackSpeed_UpgradeLevel = 0;
    public int playerAttackSpeed_UpgradeLevelMax = 3;
    public int playerAttackDamage_UpgradeLevel = 0;
    public int playerAttackDamage_UpgradeLevelMax = 5;
    [Space(20)]

    [Header("# Mana Recharge")]
    public float manaRechargeRate = 5f;
    [Space(20)]

    [Header("Item info")]
    public float item_ShieldTimer = 0;
    public float item_StopEnemyTimer = 0;

    [Header("GameObject")]
    public bool isBossSpawned;
    public Player player;

    public bool isEnemyMove = true;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (!IsLive)
        {
            return;
        }

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime && !isBossSpawned)
        {
            gameTime = maxGameTime;
            isBossSpawned = true;
        }

        ReChargeMana();
    }

    private void ReChargeMana()
    {
        if(mana < maxMana)
        {
            mana += manaRechargeRate * Time.deltaTime;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }
    }

    public void GetExp()
    {
        if (!IsLive)
            return;

        if (exp >= nextExp[playerLevel])
        {
            playerLevel++;
            exp = 0;
            int maxAbilityLevel = playerBulletSize_UpgradeLevelMax + playerAttackSpeed_UpgradeLevelMax + playerAttackDamage_UpgradeLevelMax + 1;
            if(playerLevel > maxAbilityLevel)
            {
                Time.timeScale = 0f;
                //·¹º§ÆÇ³Ú
            }
        }
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        GetExp();
    }

    public void AddHp(int hp)
    {
        health = Mathf.Clamp(health + hp, 0, maxHealth);
    }

    public void AddMana(int mana)
    {
        this.mana = Mathf.Clamp(this.mana + mana, 0, maxMana);
    }


    public void GameOver()
    {

    }
}
