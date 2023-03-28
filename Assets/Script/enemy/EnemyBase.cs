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

    Transform tran_Enemy;

    Rigidbody2D rigi_Enemy;
    Collider2D coll_Enemy;

    SpriteRenderer spri_Enemy;
    Animator anim_Enemy;

    Transform tran_target;
    public Rigidbody2D rigi_Target;
    Collider2D coll_Target;



    
    public Player TargetPlayer
    {
        protected get => player;
        set
        {
            if (player == null)
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
    float currentHP;
    bool isLive;
    int exp;

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

    protected virtual void Awake()
    {
        tran_Enemy = GetComponent<Transform>();
        rigi_Enemy = GetComponent<Rigidbody2D>();
        coll_Enemy = GetComponent<Collider2D>();
        spri_Enemy = GetComponent<SpriteRenderer>();
        anim_Enemy = GetComponent<Animator>();
        currentHP = maxHp;
    }

    float liveTime = 5;
    float baseY;
    //bool isDead = false;




    void OnEnable()
    {

    }

    protected virtual void FixedUpdate()
    { 
        player = FindObjectOfType<Player>();
        tran_target = player.transform;
        Vector2 dirVec = rigi_Target.position - rigi_Enemy.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        nextVec.y = 0;
        rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);        
        StartCoroutine(LifeOver(liveTime));

    }

    private void FixedUpdate()
    {
        Vector2 dirVec = enemysTarget.position - rigid.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        //rigid.velocity = Vector2.zero;
    }
   

    private void Update()
    {
        //transform.Translate(-Time.deltaTime*moveSpeed,0,0); //적 움직임 (현재 직선이동)
        //rigid.AddForce(Time.deltaTime * moveSpeed * 0.3f * Vector2.left,ForceMode2D.Impulse); //움직임확인중 보니까 0,0,0으로 수렴하려고함_이유확인필요
        //Debug.Log(transform.position);
        EnemyAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentHP > 0)
        {
            // Player 충돌시 Enemy HP 감소
            if (collision.gameObject.CompareTag("Skill"))
            {
               //GetEnemyHP();
                //Debug.Log($"Player HP: {player.HP}, Enemy HP: {currentHP}");
            }
        }
        // Enemy 죽이면, player 경험치 증가
        else if (currentHP < 1)
        {
            EnemyDie();
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
        currentHP -= player.attackPoint;
        //EnemyHpText.text = "HP: " + currentHP.ToString();
        //return currentHP;
    }

    
}
