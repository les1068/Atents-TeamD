using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;
    
    //Level 레벨 (+ 프로퍼티)
    byte level;
    public byte Level
    {
        get => level;
        set
        {
            level = value;

        }
    }
    //Hp 체력 (+ 프로퍼티)
    float currentHp;
    float maxHp;
    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            onHPChange?.Invoke(currentHp);
        }
    }
    //Attack 공격력
    float Attack;
    //moveSpeed 이동속도
    float moveSpeed;
    //attackSpeed 공격속도
    float attackSpeed;
    //Exp 경험치 + (프로퍼티)

    private int maxExp;
    private int currentExp;
    public int EXP
    {
        get => currentExp;
        set
        {
            currentExp = value;
            
        }
    }

    // ----------- delegate-----------
    Action<float> onHPChange;
   // ---------------------------------

    private void Update()
    {
        if(currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    // ---------------아래는 함수구현부-----------
    //스탯 초기화 (게임 시작할때)
    void InitStat()
    {
        level = 1;
        EXP = 0;
        maxExp = 10;
        HP = maxHp;
        moveSpeed = 10.0f;
        Attack = 1;
        attackSpeed = 2.0f;
    }

    public void AddHP(float plus)
    {
        HP += plus;
        Debug.Log($"현재 HP:{HP}");
    }

    public void AddExp(int plus)
    {
        EXP += plus;
        Debug.Log($"현재 EXP:{currentExp}");
    }
    void LevelUp() // 레벨업
    {
        EXP -= maxExp;
        Level += 1;
        //특성 UP규칙필요
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2; //(몇배로?->float?)
        moveSpeed *= 1.2f;
        Attack *= 1.2f;
        attackSpeed *= 1.2f;
    }   
}
