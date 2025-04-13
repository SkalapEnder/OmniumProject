using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillService : MonoBehaviour
{
    [SerializeField] private Skill[] skills;

    public void Initialize()
    {
        foreach (Skill skill in skills)
        {
            skill.Initialize();
        }
    }

    public void CancelSkills()
    {
        GameManager.Instance.IsHealingEnabled = false;
        GameManager.Instance.IsDamageBoostEnabled = false;
        GameManager.Instance.IsXPBoostEnabled = false;
        GameManager.Instance.IsAddHPEnabled = false;

            foreach (Skill skill in skills)
        {
            skill.CancelSkill();
        }
    }
}
