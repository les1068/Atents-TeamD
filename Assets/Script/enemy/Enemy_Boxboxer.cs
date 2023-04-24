using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boxboxer : Enemy_Base
{
    Collider2D coll_Enemy_Attack;
    Spawner spawner;

    protected override void Awake()
    {
        base.Awake();
        coll_Enemy_Attack = tran_Enemy.GetChild(2).GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();
        spawner = FindObjectOfType<Spawner>();
    }
    
    private void FixedUpdate()
    {
        if(tran_Target != null && isLive && !isAttack)                          //살아 있고 coll_Enemy_PlayerChecker의 트리거 안에 들어온 적이 있으면 
        {
            dirVec = tran_Target.position - tran_Enemy.position;                //타겟을 보는 방향을 구하여 
            spri_Enemy.flipX = (dirVec.x > 0) ? true : false;                   //스프라이트 플립으로 방향 전환             
            nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;      //가야할 방향 및 속도 결정 
            nextVec.y = 0;                                                      //y값은 0으로 고정(날아다니는 적의 경우  주석처리)
            if(!isHit)                                                          //맞으면 잠시 정지 
            {
                rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);         //내위치에서 가야할 방향 속도로 이동 
            }         
        }
        else if(isEnable)
        {
            IsEnable();
        }
    }
    
    protected override void SetTarget()
    {
        base.SetTarget();
        anim_Enemy.SetBool("isWalk", true);                                     //걷는 에니메이션 참으로 변경
    }

    protected override void AttackTarget()
    {
        base.AttackTarget();
        anim_Enemy.SetTrigger("Attack_Skill1");                                 //공격 에니메이션 트리거 발동
    }

    protected override void LoseTarget()
    {
        base.LoseTarget();
        anim_Enemy.SetBool("isWalk", false);                                    //걷는 애니메이션 거짓으로 변경 
    }
}
