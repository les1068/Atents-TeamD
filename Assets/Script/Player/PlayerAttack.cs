using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// 공격 속도
    /// </summary>
    protected float playerAttackSpeed = 10.0f;

    /// <summary>
    /// player 생존시 false, 사망시 true
    /// </summary>
    bool isPlayerDead = false;

    /// <summary>
    /// player 방어력
    /// </summary>
    protected float playerDefence = 30.0f;


    Player player = FindObjectOfType<Player>();
    Enemy enemy = FindObjectOfType<Enemy>();
    Skill1 skill1 = FindObjectOfType<Skill1>();

    private void OnEnable()
    {
        transform.localPosition= Vector3.zero;      // 위치 초기화
    }

    private void Update()
    {
        transform.localPosition += Time.deltaTime * playerAttackSpeed * transform.right;    // 오른쪽으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player != null)
        {
            // Enemy와 충돌시 HP 감소
            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                OnDamage();
            }
        }
        // player 사망처리
        else if (player.HP < 1) {
            isPlayerDead = true;
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        if(!isPlayerDead)
        {
            player.EXP = player.EXP - 50;   // player 사망시 경험치 감소
            gameObject.SetActive(false);
        }
    }

    protected void OnDamage()
    {
        //player.HP -= enemy.GetAttackDamage();
    }

    private void OnSkill1(InputAction.CallbackContext _)
    {
        
        
    }

    private void OnSkill2(InputAction.CallbackContext _)
    {
        
    }
    private void OnSkill3(InputAction.CallbackContext _)
    {
        
    }
}
