using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] private string skillTechName;
    [SerializeField] private int skillPrice;
    [SerializeField] private SkillType skillType;

    [SerializeField] protected Button buyButton;
    [SerializeField] private TMP_Text buttonText;
    private bool isUnlocked;

    public void Initialize() 
    {
        isUnlocked = PlayerPrefs.GetInt(skillTechName, 0) == 1 ? true : false;
        buyButton.interactable = !isUnlocked;
        buttonText.text = isUnlocked ? "Bought" : (skillPrice + " coins");

        switch(skillType)
        {
            case SkillType.Heal:
                GameManager.Instance.IsHealingEnabled = isUnlocked; 
                break;

            case SkillType.Damage:
                GameManager.Instance.IsDamageBoostEnabled = isUnlocked;
                break;

            case SkillType.XP:
                GameManager.Instance.IsXPBoostEnabled = isUnlocked;
                break;

            case SkillType.AddHP:
                GameManager.Instance.IsAddHPEnabled = isUnlocked;
                break;
        }
    }

    public virtual void BuyBoost() 
    {
        if (!checkMoney() || isUnlocked) return;

        GameManager.Instance.ScoreSystem.BuyShop(skillPrice);
        isUnlocked = true;
        PlayerPrefs.SetInt(skillTechName, 1);

        buyButton.interactable = false;
        buttonText.text = "Bought";

        switch (skillType)
        {
            case SkillType.Heal:
                GameManager.Instance.IsHealingEnabled = isUnlocked;
                break;

            case SkillType.Damage:
                GameManager.Instance.IsDamageBoostEnabled = isUnlocked;
                break;

            case SkillType.XP:
                GameManager.Instance.IsXPBoostEnabled = isUnlocked;
                break;

            case SkillType.AddHP:
                GameManager.Instance.IsAddHPEnabled = isUnlocked;
                break;
        }
    }

    public void CancelSkill()
    {
        isUnlocked = false;
        PlayerPrefs.SetInt(skillTechName, 0);

        buyButton.interactable = true;
        buttonText.text = skillPrice + " coins";
    }

    public bool checkMoney()
    {
        int playerMoney = GameManager.Instance.ScoreSystem.CoinsGlobal;
        if (playerMoney < skillPrice) return false;
        return true;
    }
}
