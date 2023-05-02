using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Boxy : Enemy_Base
{
    [SerializeField]
    AnimationCurve curve;
    public float flightSpeed = 4.0f;
    public float hoverHeight = 1.0f;

    protected virtual void FixedUpdate()
    {
        if(tran_Target != null && isLive && !isAttack)                                       //살아 있고 coll_Enemy_PlayerChecker의 트리거 안에 들어온 적이 있으면 
        {
            dirVec = tran_Target.position - tran_Enemy.position;                //타겟을 보는 방향을 구하여 
            spri_Enemy.flipX = (dirVec.x > 0) ? true : false;                   //스프라이트 플립으로 방향 전환             
            //nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;      //가야할 방향 및 속도 결정 
            //nextVec.y = 0;                                                      //y값은 0으로 고정(날아다니는 적의 경우  주석처리)
            if(!isHit)                                                          //맞으면 잠시 정지 
            {
                //rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);         //내위치에서 가야할 방향 속도로 이동 
            }         
        }
        else if (isEnable)
        {
            float forceRange;
            forceRange = UnityEngine.Random.Range(600.0f, 1400.0f);

            rigi_Enemy.AddForce(new Vector2(-1, 1) * forceRange, ForceMode2D.Force);
            isEnable = false;
        }
    }

    void Update()
    {
        
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
        //anim_Enemy.SetTrigger("Attack_Skill1_Done");
    }
}
