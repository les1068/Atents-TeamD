using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 2.0f;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 dirVec = target.position- rigid.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * enemySpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        
    }

}
