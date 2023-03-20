using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : PoolObject
{
    Player player;
    
    Transform tran_Enemy;
    
    Rigidbody2D rigi_Enemy;
    Collider2D coll_Enemy;
    
    SpriteRenderer spri_Enemy;
    Animator anim_Enemy;

    Transform tran_target;
    public Rigidbody2D rigi_Target;
    Collider2D coll_Target;

    /// <summary>
    /// 레
    /// </summary>
    public byte level;

    /// <summary>
    /// 최대  hp
    /// </summary>    
    public float maxHp;

    /// <summary>
    /// Enemy 현재 HP
    /// </summary>
    float currentHP = 100.0f;

    /// <summary>
    /// 기본 공격력 
    /// </summary>
    public float attackPoint;

    /// <summary>
    /// 기본 방어력 
    /// </summary>
    public float defencePoint;

    /// <summary>
    /// 공격속도.  공격 애니매이션 증폭에 사용 
    /// </summary>
    public float attackSpeed;

    /// <summary>
    /// 이동 속도 
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Enemy 사망시 player가 얻게 될 경험치
    /// </summary>
    public int exp;

    /// <summary>
    /// 살아 있으면 true 죽었으면 falase
    /// </summary>
    bool isLive = false;

    protected virtual void InitStat()
    {
        level = 1;
        maxHp = 100;
        attackPoint = 1.0f;
        defencePoint = 1.0f;
        attackSpeed = 1.0f;
        moveSpeed = 1.0f;
        exp = 10;
    }
            
    protected virtual void Awake()
    {
        tran_Enemy = GetComponent<Transform>();
        rigi_Enemy = GetComponent<Rigidbody2D>();
        coll_Enemy = GetComponent<Collider2D>();
        spri_Enemy = GetComponent<SpriteRenderer>();
        anim_Enemy = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        //transform.localPosition = Vector3.zero;      // 위치 초기화
    }

    void Start()
    {
       
    }
    
    protected virtual void FixedUpdate()
    {
        Vector2 dirVec = rigi_Target.position - rigi_Enemy.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        nextVec.y = 0;
        rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);        
    }

    protected virtual void Update()
    {
        EnemyAttack();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Collider2D>().CompareTag("Skill"))
        {
            Debug.Log(" 아프다 !");
        }

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
            //isLive = false;
            //EnemyDie();
        }
    }

    public TMP_Text EnemyHpText;

    /// <summary>
    /// Enemy HP 델리게이트
    /// </summary>
    public Action<int> onChangeEnemyHP;

    /// <summary>
    ///  몬스터 공격력 가져오기 
    /// </summary>
    /// <returns></returns>
    public float EnemyAttack()
    {
        return attackPoint;
    }

    private void onDamageEnemy()
    {
        currentHP = currentHP - player.EXP;     // player Attack 접근 불가. 임의로 EXP 입력
        EnemyHpText.text = "HP: " + currentHP.ToString();
    }

    void EnemyDie()
    {
        if (!isLive)
        {
            player.AddExp((int)exp);    // playerStat의 exp는 int. Enemy의 exp는 float. player에 exp 추가
            gameObject.SetActive(false);    // Enemy 비활성화
        }
    }

    public float GetEnemyHP()
    {
        return currentHP;
    }    
}
