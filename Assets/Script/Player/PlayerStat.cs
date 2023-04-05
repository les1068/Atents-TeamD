using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;
    
    //Level 관련 (+ 프로퍼티)
    byte level;
    public byte Level
    {
        get => level;
        set
        {
            level = value;

        }
    }
    //Hp 관련 (+ 프로퍼티)
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
    //attack 공격력
    protected float attack;
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

    // --------------함수시작-----------
    //초기스탯
    void InitStat()
    {
        level = 1;
        EXP = 0;
        maxExp = 10;
        HP = maxHp;
        moveSpeed = 10.0f;
        attack = 1;
        attackSpeed = 2.0f;
    }

    public void AddHP(float plus)
    {
        HP += plus;
        Debug.Log($"���� HP:{HP}");
    }

    public void AddExp(int plus)
    {
        EXP += plus;
        Debug.Log($"���� EXP:{currentExp}");
    }
    void LevelUp() // 레벨업
    {
        EXP -= maxExp;
        Level += 1;
        //레벨업시 어떻게 변화할지는 의논필요
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2; //부드러운 경험치 bar를 위해 float으로 변경해야할지?
        moveSpeed *= 1.2f;
        attack *= 1.2f;
        attackSpeed *= 1.2f;
    }   
}
