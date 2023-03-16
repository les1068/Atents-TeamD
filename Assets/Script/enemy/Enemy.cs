using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : PoolObject
{
    public float enemySpeed = 2.0f;
    public Rigidbody2D enemysTarget;
    protected Transform target;

    bool isLive;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Collider2D collider2D = GetComponent<Collider2D>();
        
    }
    private void FixedUpdate()
    {
        Vector2 dirVec = enemysTarget.position - rigid.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * enemySpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {

    }

    /// <summary>
    /// Enemy 최대 HP
    /// </summary>
    protected float maxHP = 100.0f;

    /// <summary>
    /// Enemy 현재 HP
    /// </summary>
    protected float currentHP = 100.0f;

    public float GetEnemyHP()
    {
        return currentHP;
    }

    public TMP_Text EnemyHpText;

    /// <summary>
    /// Enemy 사망시 player가 얻게 될 경험치
    /// </summary>
    protected float exp = 30.0f;

    /// <summary>
    /// Enemy 공격력
    /// </summary>
    protected float attackDamage = 10.0f;

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    /// <summary>
    /// Enemy 방어력
    /// </summary>
    protected float EnemyDefence = 20.0f;

    /// <summary>
    /// Enemy 이동속도
    /// </summary>
    protected float moveSpeed = 10.0f;

    /// <summary>
    /// Enemy 공격 속도
    /// </summary>
    protected float enemyAttackSpeed = 8.0f;

    /// <summary>
    /// Enemy 생존시 flase, 사망시 true
    /// </summary>
    bool isEnemyDead = false;

    /// <summary>
    /// Enemy HP 델리게이트
    /// </summary>
    public Action<int> onChangeEnemyHP;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        target = player.transform;
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;      // 위치 초기화
    }

    private void Update()
    {
        EnemyAttack();
    }

    private void EnemyAttack()
    {
        // 플레이어 방향으로 공격하는 함수 만들기
        //transform.localPosition += Time.deltaTime * enemyAttackSpeed * -transform.right;    // 왼쪽으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentHP != 0)
        {
            // Player 충돌시 Enemy HP 감소
            /*if (collision.gameObject.CompareTag("PlayerAttack"))
            {
                onDamageEnemy();
            }*/
        }
        // Enemy 죽이면, player 경험치 증가
        else if (currentHP < 1)
        {
            isEnemyDead = true;
            EnemyDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collider2D>().CompareTag("Skill"))
        {
            Debug.Log(" 아프다 !");
        }
        
    }


    void EnemyDie()
    {
        if (!isEnemyDead)
        {
            player.AddExp((int)exp);    // playerStat의 exp는 int. Enemy의 exp는 float. player에 exp 추가
            gameObject.SetActive(false);    // Enemy 비활성화
        }
    }

    private void onDamageEnemy()
    {
        currentHP = currentHP - player.EXP;     // player Attack 접근 불가. 임의로 EXP 입력
        EnemyHpText.text = "HP: " + currentHP.ToString();
    }
}
