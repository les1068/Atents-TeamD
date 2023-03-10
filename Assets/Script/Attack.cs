using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    /// <summary>
    /// ���� �ӵ�
    /// </summary>
    protected float palyerAttackSpeed = 10.0f;

    /// <summary>
    /// player ������ flase, ����� true
    /// </summary>
    bool isPlayerDead = false;

    /// <summary>
    /// player ����
    /// </summary>
    protected float playerDefence = 30.0f;


    Player player = FindObjectOfType<Player>();
    PlayerStat playerStat = FindObjectOfType<PlayerStat>();
    Enemy enemy = FindObjectOfType<Enemy>();
    Skill1 skill1 = FindObjectOfType<Skill1>();

    private void OnEnable()
    {
        transform.localPosition= Vector3.zero;      // ��ġ �ʱ�ȭ
    }

    private void Update()
    {
        transform.localPosition += Time.deltaTime * palyerAttackSpeed * transform.right;    // ���������� �̵�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player != null)
        {
            // Enemy�� �浹�� HP ����
            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                OnDamage();
            }
        }
        // player ���ó��
        else if (playerStat.HP < 1) {
            isPlayerDead = true;
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        if(!isPlayerDead)
        {
            playerStat.EXP = playerStat.EXP - 50;   // player ����� ����ġ ����
            gameObject.SetActive(false);
        }
    }

    protected void OnDamage()
    {
        playerStat.HP -= enemy.GetAttackDamage();
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
