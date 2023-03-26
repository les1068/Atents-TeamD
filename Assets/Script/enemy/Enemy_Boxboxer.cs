using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Boxboxer : PoolObject
{
    Player player;
    Skill1 skill1;
    Transform tran_Enemy;
    Rigidbody2D rigi_Enemy;
    Collider2D coll_Enemy;
    
    SpriteRenderer spri_Enemy;
    Animator anim_Enemy;

    Transform tran_Target = null;
    Rigidbody2D rigi_Target;
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

    float pSkillPoint;

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
        player = FindObjectOfType<Player>();             
    }

    private void OnEnable()
    {
        
    }

    void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        if(tran_Target != null)
        {
            Vector2 dirVec = tran_Target.position - tran_Enemy.position;   // 타겟포지션 - 나의 포지션
            Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
            nextVec.y = 0;
            rigi_Enemy.MovePosition(rigi_Enemy.position + nextVec);
        }
    }

    void Update()
    {
        //EnemyAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Skill1")
        {
            pSkillPoint = collision.gameObject.GetComponent<Skill1>().skillpoint;
        }
        else if (collision.gameObject.tag == "Skill2")
        {
            pSkillPoint = collision.gameObject.GetComponent<Skill2>().skillpoint;
        }
        else if (collision.gameObject.tag == "Skill3")
        {
            pSkillPoint = collision.gameObject.GetComponent<Bullet>().skillpoint;
        }
        Hit_Start_Enemy();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Skill1")
        {
            pSkillPoint = collision.gameObject.GetComponent<Skill1>().skillpoint;
        }
        else if (collision.gameObject.tag == "Skill2")
        {
            pSkillPoint = collision.gameObject.GetComponent<Skill2>().skillpoint;
        }
        else if (collision.gameObject.tag == "Skill3")
        {
            pSkillPoint = collision.gameObject.GetComponent<Bullet>().skillpoint;
        }
        Hit_Start_Enemy();
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

    private void Hit_Start_Enemy()
    {
        float pAttackPoint = player.attackPoint;
        float skillPower = pSkillPoint * pAttackPoint;
        currentHP -= skillPower;
        Debug.Log($"{currentHP}");
        if (currentHP < 0)
        {
            Die_Enemy();            
        }
        else
        {
            anim_Enemy.SetBool("isHiT", true);
        }
        
        //EnemyHpText.text = "HP: " + currentHP.ToString();
    }

    private void Hit_End_Enemy()
    {
        anim_Enemy.SetBool("isHiT", false);
    }

    void Die_Enemy()
    {
        anim_Enemy.SetTrigger("isDie");
        gameObject.SetActive(false);    // Enemy 비활성화
        Destroy(gameObject,0.5f);

        //player.AddExp((int)exp);    // playerStat의 exp는 int. Enemy의 exp는 float. player에 exp 추가
    }

    public float GetEnemyHP()
    {
        return currentHP;
    }
}
