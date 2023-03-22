using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    /// <summary>
    /// 레
    /// </summary>
    
    public byte level;
    
    /// <summary>
    /// 기본 hp
    /// </summary>    
    public float maxHp;

    /// <summary>
    /// 가본 공격력 
    /// </summary>
    public float attackPoint;

    /// <summary>
    /// 기본 방어력 
    /// </summary>
    public float defencePoint;

    /// <summary>
    /// 공격속도.  공격 애니매이션 증폭에 사용 
    /// </summary>
    public float attackSpeed;
    
    /// <summary>
    /// 이동 속도
    /// </summary>
    protected float moveSpeed = 1.0f;

    protected virtual void InitStat()
    {
        level = 1;
        maxHp = 100;
        attackPoint = 1.0f;
        defencePoint = 1.0f;
        attackSpeed = 1.0f;
    }


}
