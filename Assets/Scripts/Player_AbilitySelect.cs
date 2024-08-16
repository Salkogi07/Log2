using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilitySelect : MonoBehaviour
{
    public GameObject itemPanel;

    public void UpgradeSize()
    {
        if (GameManager.instance.playerBulletSize_UpgradeLevel < GameManager.instance.playerBulletSize_UpgradeLevelMax)
        {
            GameManager.instance.playerBullet_scale += .4f;
            GameManager.instance.playerBulletSize_UpgradeLevel++;
            itemPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void UpgradeSpeed()
    {
        if (GameManager.instance.playerAttackSpeed_UpgradeLevel < GameManager.instance.playerAttackSpeed_UpgradeLevelMax)
        {
            GameManager.instance.playerAttack_cooldown -= 0.2f;
            GameManager.instance.playerAttackSpeed_UpgradeLevel++;
            itemPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void UpgradeDamage()
    {
        if (GameManager.instance.playerAttackDamage_UpgradeLevel < GameManager.instance.playerAttackDamage_UpgradeLevelMax)
        {
            GameManager.instance.playerAttack_damage += 8;
            GameManager.instance.playerAttackDamage_UpgradeLevel++;
            itemPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
