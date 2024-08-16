using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Player player;
    public bool isBossSpawned;
    public GameObject bossObj;

    public GameObject HUDPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject levelUpPanel;

    public GameObject EnemySetPanel;
    public GameObject ItmeSetPanel;

    public GameObject skillstopObj;

    public BackgroundSound sound;
    public LevelUpSound levelUpSound;

    public Spawner spawner;


    public bool isEnemyMove = true;
    public float Enemy_SkillTime = 0;


    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        sound.NoBossSound();
        health = maxHealth;
        Time.timeScale = 1f;
        IsLive = true;
        HUDPanel.SetActive(true);
    }

    private void Update()
    {
        MasterKey();

        if (!IsLive)
        {
            return;
        }

        Enemy_SkillTime -= Time.deltaTime;
        skillstopObj.SetActive(Is_SkillStopEnemy());

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime && !isBossSpawned)
        {
            gameTime = maxGameTime;
            isBossSpawned = true;
            SpanwBoss();
        }

        ReChargeMana();
    }

    private void MasterKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentIndex = currentScene.buildIndex;

            if (currentIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            else if (currentIndex == 2)
            {
                SceneManager.LoadScene(3);
            }
            else if (currentIndex == 3)
            {
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            exp += 100000000;
            GetExp();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            isMasterDamge = !isMasterDamge;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            health = maxHealth;
            mana = maxMana;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            EnemySetPanel.SetActive(false);
            ItmeSetPanel.SetActive(!ItmeSetPanel.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            ItmeSetPanel.SetActive(false);
            EnemySetPanel.SetActive(!EnemySetPanel.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemyObj);
            }
        }
    }

    private void SpanwBoss()
    {
        foreach(GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemyObj);
        }
        sound.BossSound();
        Instantiate(bossObj,player.transform.position + Vector3.up * 4, Quaternion.identity);
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
            if(playerLevel < maxAbilityLevel)
            {
                Time.timeScale = 0f;
                levelUpSound.LevelPlaySound();
                levelUpPanel.SetActive(true);
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

    public void Use_ItemSheild()
    {
        item_ShieldTimer = 3;
    }
    public bool Is_ItemSheild()
    {
        return item_ShieldTimer > 0;
    }

    public void Use_ItemStopEnemy()
    {
        item_StopEnemyTimer = 1;
    }
    public bool Is_ItemStopEnemy()
    {
        return item_StopEnemyTimer > 0;
    }

    public void Use_SkillStopEnemy()
    {
        Enemy_SkillTime = 1;
    }
    public bool Is_SkillStopEnemy()
    {
        return Enemy_SkillTime > 0;
    }

    public void GameWin()
    {
        StartCoroutine(GameWinRoutine());
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameWinRoutine()
    {
        sound.StopSound();
        IsLive = false;
        HUDPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator GameOverRoutine()
    {
        sound.StopSound();
        IsLive = false;
        HUDPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Stage1()
    {
        SceneManager.LoadScene(1);
    }

    public void Stage2()
    {
        SceneManager.LoadScene(2);
    }

    public void Stage3()
    {
        SceneManager.LoadScene(3);
    }

    public void Ending()
    {
        SceneManager.LoadScene(4);
    }

    public void SpawnCheatEnemy(int level)
    {
        int enemyPos = Random.Range(1, spawner.spanwpoint.Length);
        Instantiate(spawner.prefab[level], spawner.spanwpoint[enemyPos].position, Quaternion.identity);
    }

    public void UseItem(int type)
    {
        InfoType infoType = (InfoType)type;
        switch (infoType)
        {
            case InfoType.Exp:
                AddExp(30);
                break;
            case InfoType.Speed:
                SpeedUP();
                break;
            case InfoType.Shield:
                Use_ItemSheild();
                break;
            case InfoType.Heal:
                AddHp(30);
                AddMana(20);
                break;
            case InfoType.Stop:
                Use_ItemStopEnemy();
                break;
        }
    }

    void SpeedUP()
    {
        if (speedCount < 5)
        {
            speed += 0.5f;
        }
        else
        {
            AddExp(20);
        }
        speedCount++;
    }
}
