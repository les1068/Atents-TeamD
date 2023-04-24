using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Batafire : Enemy_Base
{        
    /// <summary>
    /// 적 위아래 이동 정도(비율)
    /// </summary>
    [Range(0.1f, 3.0f)]         // 변수 범위를 (min,max)사이로 변경시키는 슬라이더 추가
    public float amplitude = 1; // 사인 결과값을 증폭시킬 변수(위아래 차이 결정)

    /// <summary>
    /// 위아래로 한번 움직이는데 걸리는 거리(비율)
    /// </summary>
    public float frequency = 1; // 사인 그래프가 한번 도는데 걸리는 시간(가로 폭 결정)

    /// <summary>
    /// 누적 시간(사인 계산용)
    /// </summary>
    float timeElapsed = 0.0f;

    /// <summary>
    /// y좌표 초기값 
    /// </summary>
    float baseY = 0.0f;

    private void FixedUpdate()
    {   
        if (tran_Target != null && isLive)                                      // 살아 있고 coll_Enemy_PlayerChecker의 트리거 안에 들어온 적이 있으면 
        {
            dirVec = tran_Target.position - tran_Enemy.position;                // 타겟을 보는 방향을 구하여 
            spri_Enemy.flipX = (dirVec.x > 0) ? true : false;                   // 스프라이트 플립으로 방향 전환             
            nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;      // 가야할 방향 및 속도 결정 
                        
            rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);             // 내위치에서 가야할 방향 속도로 이동
        }
        else
        {            
            baseY = tran_Enemy.position.y;
            rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);             // 내위치에서 가야할 방향 속도로 이동

            timeElapsed += Time.fixedDeltaTime * frequency;                     // frequency에 비례해서 시간 증가가 빠르게 된다
            float y = baseY + Mathf.Sin(timeElapsed) * amplitude;               // y는 시작위치에서 sin 결과값만큼 변경

            if (tran_Enemy.position.x > -10.0f && !spri_Enemy.flipX)            // 
            {
                spri_Enemy.flipX = false;
                float x = transform.position.x - moveSpeed * Time.fixedDeltaTime;
                nextVec = new Vector3(x, y, 0);
            }
            else if (tran_Enemy.position.x > 10.0f && spri_Enemy.flipX)
            {
                spri_Enemy.flipX = false;
                float x = transform.position.x - moveSpeed * Time.fixedDeltaTime;
                nextVec = new Vector3(x, y, 0);
            }
            else
            {
                spri_Enemy.flipX = true;
                float x = transform.position.x + moveSpeed * Time.fixedDeltaTime;
                nextVec = new Vector3(x, y, 0);
            }
            rigi_Enemy.MovePosition(nextVec);
        }
    }

    protected override void SetTarget()
    {
        base.SetTarget();
        anim_Enemy.SetBool("isDash1", true);                                    //걷는 에니메이션 참으로 변경
    }

    protected override void AttackTarget()
    {
        base.AttackTarget();
        anim_Enemy.SetTrigger("Attack_Skill1");                                 //공격 에니메이션 트리거 발동
    }

    protected override void LoseTarget()
    {
        base.LoseTarget();
        anim_Enemy.SetBool("isDash1", false);                                   //걷는 애니메이션 거짓으로 변경 
    }
}
