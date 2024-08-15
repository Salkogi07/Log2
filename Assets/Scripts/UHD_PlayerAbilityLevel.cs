using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UHD_PlayerAbilityLevel : MonoBehaviour
{
    public enum InfoType { Size, Cooldowm, Damage }
    public InfoType type;

    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Size:
                if (GameManager.instance.playerBulletSize_UpgradeLevel == 5)
                {
                    myText.text = "Lv.Max";
                }
                else
                {
                    myText.text = string.Format("Lv.{0:F0}/5", GameManager.instance.playerBulletSize_UpgradeLevel);
                }
                myText.text = myText.text + "\n�Ѿ��� ũ�Ⱑ �����մϴ�.";
                break;
            case InfoType.Cooldowm:
                if (GameManager.instance.playerAttackSpeed_UpgradeLevel == 3)
                {
                    myText.text = "Lv.Max";
                }
                else
                {
                    myText.text = string.Format("Lv.{0:F0}/3", GameManager.instance.playerAttackSpeed_UpgradeLevel);
                }
                myText.text = myText.text + "\n���� ������ �����մϴ�.";
                break;
            case InfoType.Damage:
                if (GameManager.instance.playerAttackDamage_UpgradeLevel == 5)
                {
                    myText.text = "Lv.Max";
                }
                else
                {
                    myText.text = string.Format("Lv.{0:F0}/5", GameManager.instance.playerAttackDamage_UpgradeLevel);
                }
                myText.text = myText.text + "\n���� �������� �����մϴ�.";
                break;
        }
    }
}
