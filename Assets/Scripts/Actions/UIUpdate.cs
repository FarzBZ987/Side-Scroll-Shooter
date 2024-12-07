using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image skillBar;
    [SerializeField] private Image ultimateBar;

    private void UpdateHealth(int val)
    {
        healthUI.fillAmount = ((75f - (float)val) / 75f);

        healthText.text = val.ToString();
    }

    private void UpdateSkill(float val)
    {
        skillBar.fillAmount = Mathf.Floor(val) / 5;
    }

    private void UpdateUltimate(float val)
    {
        ultimateBar.fillAmount = val / 10;
    }

    private void OnEnable()
    {
        Player.onHealthChangeEvent += UpdateHealth;
        Player.onSkillChangeEvent += UpdateSkill;
        Player.onUltimateChangeEvent += UpdateUltimate;
    }

    private void OnDisable()
    {
        Player.onHealthChangeEvent -= UpdateHealth;
        Player.onSkillChangeEvent -= UpdateSkill;
        Player.onUltimateChangeEvent -= UpdateUltimate;
    }
}