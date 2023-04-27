using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float speed = 10.0f;
    public float attackPoint=10.0f;

    Rigidbody rigid;
    Animator anim_Enemy;

    BossAttack bossAttack;
    float enemyattack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * speed * -transform.right;
    }
    
    public float Attack_Enemy()
    {
        return attackPoint;
    }
}
