using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Hp, Mana, Kill, Exp, Level,Time}
    public InfoType type;

    Text text;
    Image slider;

    private void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Hp:
                float currentHp = GameManager.instance.health;
                float maxHp = GameManager.instance.maxHealth;
                slider.fillAmount = currentHp / maxHp;
                break;
            case InfoType.Mana:
                float currentMana = GameManager.instance.mana;
                float maxMana = GameManager.instance.maxMana;
                slider.fillAmount = currentMana / maxMana;
                break;
            case InfoType.Kill:
                text.text = string.Format("{0:F0}", GameManager.instance.killEnemyCount);
                break;
            case InfoType.Exp:
                float currentExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.playerLevel];
                slider.fillAmount = currentExp / maxExp;
                break;
            case InfoType.Level:
                text.text = string.Format("Lv.{0:F0}", GameManager.instance.playerLevel);
                break;
            case InfoType.Time:
                float realTime = Mathf.Max(GameManager.instance.maxGameTime - GameManager.instance.gameTime, 0);
                int min = Mathf.FloorToInt(realTime / 60);
                int sec = Mathf.FloorToInt(realTime % 60);
                text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
        }
    }


}
