using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    public static Action onAttackClicked;
    public static Action onSkillClicked;
    public static Action onUltimateClicked;

    public void Attack()
    {
        onAttackClicked?.Invoke();
    }

    public void Skill()
    {
        onSkillClicked?.Invoke();
    }

    public void Ultimate()
    {
        onUltimateClicked?.Invoke();
    }
}