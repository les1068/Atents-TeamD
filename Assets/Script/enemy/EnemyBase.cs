using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyBase : PoolObject
{
    public Rigidbody2D enemysTarget;
    protected Transform target;
    Rigidbody2D rigid;
    Player player;
    
    public Player TargetPlayer
    {
        protected get => player;
        set
        {
            if(player == null)
            {
                player = value;
            }
        }
    }
    
    [Header("상태관련----------------------")]
    /// <summary>
    /// 레벨
    /// </summary>

    public byte level;

    /// <summary>
    /// 기본 hp
    /// </summary>    
    public float maxHp;

    /// <summary>
    /// 가본 공격력 
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
    public float moveSpeed = 1.0f;

    protected virtual void InitStat()
    {
        level = 1;
        maxHp = 100;
        attackPoint = 1.0f;
        defencePoint = 1.0f;
        attackSpeed = 1.0f;
    }

    /// <summary>
    /// Enemy 사망시 player가 얻게 될 경험치
    /// </summary>
    public int exp = 10;

    /// <summary>
    /// 살아 있으면 true 죽었으면 falase
    /// </summary>
    bool isLive = false;
    float liveTime = 5;
    float baseY;

    private void Awake()
    {
       
        rigid = GetComponent<Rigidbody2D>();
        Collider2D collider2D = GetComponent<Collider2D>();        
    }

    void OnEnable()
    {
        
        player = FindObjectOfType<Player>();
        target = player.transform;
        StartCoroutine(LifeOver(liveTime));

    }

    private void FixedUpdate()
    {
        Vector2 dirVec = enemysTarget.position - rigid.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void Update()
    {
        transform.Translate(-Time.deltaTime*moveSpeed,0,0); //적 움직임 (현재 직선이동)
        //rigid.AddForce(Time.deltaTime * moveSpeed * 0.3f * Vector2.left,ForceMode2D.Impulse); //움직임확인중 보니까 0,0,0으로 수렴하려고함_이유확인필요
        //Debug.Log(transform.position);
        EnemyAttack();
    }

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



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<Collider2D>().CompareTag("Skill"))
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
            isLive = false;
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        if (!isLive)
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
