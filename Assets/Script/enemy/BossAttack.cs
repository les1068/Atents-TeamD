using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float speed = 10.0f;
    public float attackPoint;

    Rigidbody rigid;
    Animator anim_Enemy;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * speed * -transform.right;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
    public float Attack_Enemy()
    {
        return attackPoint;
    }
}
