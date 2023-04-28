using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Canvas_Player : MonoBehaviour
{
    Image imag_Skill1;
    Image imag_Skill2;
    Image imag_Skill3;
    TextMeshProUGUI text_Skill1;
    TextMeshProUGUI text_Skill2;
    TextMeshProUGUI text_Skill3;
    TextMeshProUGUI text_Score;
    TextMeshProUGUI text_Exp;
    Slider sliderHP;

    Player player;
    Skill1 skill1;
    Skill2 skill2;
    Skill3 skill3;

    float currentScore = 0.0f;
    int targetScore = 0;

    float currentExp = 0.0f;
    int targetExp = 0;

    private void Awake()
    {
        Transform tran_Skill1 = transform.GetChild(1);
        Transform tran_Skill2 = transform.GetChild(2);
        Transform tran_Skill3 = transform.GetChild(3);

        imag_Skill1 = tran_Skill1.GetComponent<Image>();
        imag_Skill2 = tran_Skill2.GetComponent<Image>();
        imag_Skill3 = tran_Skill3.GetComponent<Image>();
        text_Skill1 = tran_Skill1.GetComponentInChildren<TextMeshProUGUI>();
        text_Skill2 = tran_Skill2.GetComponentInChildren<TextMeshProUGUI>();
        text_Skill3 = tran_Skill3.GetComponentInChildren<TextMeshProUGUI>();

        sliderHP = transform.GetChild(4).GetChild(1).GetComponent<Slider>();

        Transform tran_Text = transform.GetChild(5);
        
        Transform tran_Score = tran_Text.GetChild(0);
        text_Score = tran_Score.GetComponent<TextMeshProUGUI>();
        Transform tran_Exp = tran_Text.GetChild(1);
        text_Exp = tran_Exp.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        skill1 = FindObjectOfType<Skill1>();
        skill2 = FindObjectOfType<Skill2>();
        skill3 = FindObjectOfType<Skill3>();

        imag_Skill1.fillAmount = 1;
        imag_Skill2.fillAmount = 1;
        imag_Skill3.fillAmount = 1;

        text_Skill1.text = skill1.skillComboMax.ToString();
        text_Skill2.text = skill2.skillComboMax.ToString();
        text_Skill3.text = skill3.skillComboMax.ToString();

        skill1.onSkillComboChange += Refresh_Skill1Combo;
        skill2.onSkillComboChange += Refresh_Skill2Combo;
        skill3.onSkillComboChange += Refresh_Skill3Combo;

        skill1.onSkillCoolTimeChange += Refresh_Skill1CoolTime;
        skill2.onSkillCoolTimeChange += Refresh_Skill2CoolTime;
        skill3.onSkillCoolTimeChange += Refresh_Skill3CoolTime;

        player = FindObjectOfType<Player>();

        player.onScoreChange += Refresh_Score;
        player.onEXPChange += Refresh_Exp;

        text_Score.text = currentScore.ToString();
        text_Exp.text = currentExp.ToString();
        sliderHP.minValue = 0f;
        sliderHP.maxValue = player.maxHp;
        sliderHP.value = player.maxHp;
    }

    private void Update()
    {
        text_Score.text = $"{targetScore}";
        text_Exp.text = $"{targetExp}";
        sliderHP.value = player.HP;
    }

    void Refresh_Score(int newScore)
    {        
        targetScore = newScore;
    }

    void Refresh_Exp(int newExp)
    {
        targetExp = newExp;        
    }

    void Refresh_Skill1CoolTime(float SkillCoolTime)
    {
        imag_Skill1.fillAmount = SkillCoolTime;
    }

    void Refresh_Skill2CoolTime(float SkillCoolTime)
    {
        imag_Skill2.fillAmount = SkillCoolTime;
    }

    void Refresh_Skill3CoolTime(float SkillCoolTime)
    {
        imag_Skill3.fillAmount = SkillCoolTime;
    }

    void Refresh_Skill1Combo(int SkillCombo)
    {
        text_Skill1.text = SkillCombo.ToString();
    }

    void Refresh_Skill2Combo(int SkillCombo)
    {
        text_Skill2.text = SkillCombo.ToString();
    }

    void Refresh_Skill3Combo(int SkillCombo)
    {
        text_Skill3.text = SkillCombo.ToString();
    }
}
