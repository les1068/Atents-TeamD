using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;
    
    //Level ���� (+ ������Ƽ)
    byte level;
    public byte Level
    {
        get => level;
        set
        {
            level = value;

        }
    }
    //Hp ü�� (+ ������Ƽ)
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
    //Attack ���ݷ�
    protected float attack;
    //moveSpeed �̵��ӵ�
    float moveSpeed;
    //attackSpeed ���ݼӵ�
    float attackSpeed;
    //Exp ����ġ + (������Ƽ)

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

    // ---------------�Ʒ��� �Լ�������-----------
    //���� �ʱ�ȭ (���� �����Ҷ�)
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
    void LevelUp() // ������
    {
        EXP -= maxExp;
        Level += 1;
        //Ư�� UP��Ģ�ʿ�
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2; //(����?->float?)
        moveSpeed *= 1.2f;
        attack *= 1.2f;
        attackSpeed *= 1.2f;
    }   
}
