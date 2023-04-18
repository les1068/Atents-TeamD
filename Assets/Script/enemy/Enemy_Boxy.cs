using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Boxy : Enemy_Base
{
    /// <summary>
    /// 곡선 이동용 
    /// </summary>
    [SerializeField]
    AnimationCurve curve;
    public float flightSpeed = 4.0f;
    public float hoverHeight = 1.0f;

    private void FixedUpdate()
    {
        if(tran_Target != null && isLive && !isAttack)                          //살아 있고 coll_Enemy_PlayerChecker의 트리거 안에 들어온 적이 있으면 
        {
            dirVec = tran_Target.position - tran_Enemy.position;                //타겟을 보는 방향을 구하여 
            spri_Enemy.flipX = (dirVec.x > 0) ? true : false;                   //스프라이트 플립으로 방향 전환             
        }
        else if (isEnable)
        {
            IsEnable();
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
        StartCoroutine(IEFlight());
    }

    private IEnumerator IEFlight()
    {
        float duration = flightSpeed;
        float time = 0.0f;
        Vector3 start = tran_Enemy.position;
        Vector3 end = tran_Target.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }        
        isAttack = false;        
    }
}
