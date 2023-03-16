using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill1 : MonoBehaviour
{
    Player player;    
    bool doingAttack;
    Transform skilltrans;

    void AttackStart()
    {
        doingAttack = true;
    }
    void AttackEnd()
    {
        doingAttack = false;
        Destroy(gameObject, 0.01f);
    }
    
    /// <summary>
    /// 스킬 데미지 계산용 변수
    /// </summary>
    public float skillValue = 1.0f;

    /// <summary>
    /// 스킬 데미지 계산 후 변수 
    /// </summary>
    public float skillPower
    {
        get => skillPower;
        set
        {
            skillPower = value * skillValue * player.attackPoint;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        skilltrans = GetComponent<Transform>();
    //    Collider2D collider2D = GetComponentInChildren<Collider2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        skilltrans.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && doingAttack)
        {
        //    Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
        }

        //플레이어의 공격력
    }
   
}