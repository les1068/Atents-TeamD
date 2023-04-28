using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Base : PoolObject
{
    protected Transform tran_Enemy;
    protected Rigidbody2D rigi_Enemy;
    protected Collider2D coll_Enemy;
    protected SpriteRenderer spri_Enemy;
    protected Animator anim_Enemy;
    protected Collider2D coll_Enemy_PlayerChecker;

    protected Player player;
    protected Skill1 skill1;
    protected Skill2 skill2;
    protected Bullet bullet;

    protected Transform tran_Target;    

    protected Vector2 dirVec;
    protected Vector2 nextVec;

    /// <summary>
    /// 레벨
    /// </summary>
    protected byte level;

    /// <summary>
    /// 최대hp
    /// </summary>    
    public float maxHp;

    /// <summary>
    /// Enemy 현재 HP
    /// </summary>
    protected float currentHP;
    public float HP
    {
        get => currentHP;
        set
        {
            currentHP = value;
        }
    }
    /// <summary>
    /// Enemy HP 델리게이트
    /// </summary>
    public Action<float> onChangeEnemyHP;

    /// <summary>
    /// 기본 공격력 
    /// </summary>
    public float attackPoint;
    public float AttackPoint
    {
        get => attackPoint;
        set
        {
            attackPoint = value;
        }
    }

    /// <summary>
    /// 기본 방어력 
    /// </summary>
    public float defencePoint;

    /// <summary>
    /// 공격속도.  공격 애니매이션 증폭에 사용 
    /// </summary>
    public float attackSpeed;

    /// <summary>
    /// enemy가 공격으로 넘어가기 위한 기준 거리 
    /// </summary>
    public float skill_Range = 2.0f;

    /// <summary>
    /// 이동 속도 
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Enemy 사망시 player가 얻게 될 경험치
    /// </summary>
    public int exp;

    /// <summary>
    /// 맞고 있으면 true 아니면 falase
    /// </summary>
    protected bool isHit = false;

    /// <summary>
    /// 살아 있으면 true 죽었으면 falase
    /// </summary>
    protected bool isLive = true;

    /// <summary>
    /// 공격중이면 true 아니면 falase
    /// </summary>
    protected bool isAttack = false;

    /// <summary>
    /// 스폰에서 Enable 될떄 true
    /// </summary>
    protected bool isEnable = false;

    /// <summary>
    /// 데미지 계산용 player SkillPoint
    /// </summary>
    protected float pSkillPoint;

    /// <summary>
    /// enemy hp 바(UI)
    /// </summary>
    public TMP_Text EnemyHpText;

    protected virtual void Awake()
    {
        tran_Enemy = GetComponent<Transform>();
        rigi_Enemy = GetComponent<Rigidbody2D>();
        coll_Enemy = GetComponent<Collider2D>();
        spri_Enemy = GetComponent<SpriteRenderer>();
        anim_Enemy = GetComponent<Animator>();        
        coll_Enemy_PlayerChecker = GetComponentInChildren<CircleCollider2D>();
    }

    protected virtual void OnEnable()
    {
        InitStat();                                                             //초기화
        isHit = false;
        isLive = true;        
        tran_Target = null;
        isEnable = true;
    }

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        skill1 = FindObjectOfType<Skill1>();
        skill2 = FindObjectOfType<Skill2>();
        bullet = FindObjectOfType<Bullet>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)                                    //skill layer의 tirger와 충돌 시 
        {
            GameObject obj = collision.gameObject;
            if (collision.CompareTag("Skill1"))                                 //어떤 스킬인지 확인하여 
            {
                pSkillPoint = skill1.skillpoint;                                // 해당 스킬의 skillpoint 를 받아옴 
            }
            else if (collision.CompareTag("Skill2"))
            {
                pSkillPoint = skill2.skillpoint;
            }
            else if (collision.CompareTag("Skill3"))
            {
                pSkillPoint = bullet.skillpoint;
            }
            Hit_Enemy();                                                        //맞는 처리
        }
        else if (collision.gameObject.layer == 7)                               //player layer가 triger와 충돌 시  
        {
            GameObject obj = collision.gameObject;
            tran_Target = obj.transform;                                        //타겟으로 설정            
            SetTarget();
        }
        isAttack = false;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        isHit = false;
        if (collision.gameObject.layer == 7)                                    //player layer가 triger에 머물 시  
        {
            GameObject obj = collision.gameObject;
            tran_Target = obj.transform;                                        //타겟으로 설정                                         
            dirVec = tran_Target.position - tran_Enemy.position;                //타겟과의 거리를 구하여 
            if (dirVec.x < skill_Range && !isAttack)                            //기준 거리보다 가깝고 공격중이 아니면  
            {                
                AttackTarget();
            }
        }
        isAttack = false;
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        isHit = false;
        if (collision.gameObject.layer == 7)                                    //player layer가 triger에서 나가면   
        {
            LoseTarget();
        }
        isAttack = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)                                    //skill layer의 collision 과 충돌 시 
        {
            GameObject obj = collision.gameObject;                              //triger와 동일한 구조 
            if (obj.CompareTag("Skill1"))
            {
                pSkillPoint = skill1.skillpoint;
            }
            else if (obj.CompareTag("Skill2"))
            {
                pSkillPoint = skill2.skillpoint;
            }
            else if (obj.CompareTag("Skill3"))
            {
                pSkillPoint = bullet.skillpoint;
            }
            Hit_Enemy();
        }
    }

    /// <summary>
    /// 스텟 초기화용
    /// </summary>
    protected virtual void InitStat()
    {
        HP = maxHp;
    }

    protected virtual void IsEnable()
    {
        isEnable = false;
        float forceRange;
        forceRange = UnityEngine.Random.Range(600.0f, 1400.0f);
        rigi_Enemy.AddForce(new Vector2(-1, 1) * forceRange, ForceMode2D.Force);
    }

    protected virtual void SetTarget()
    {
        
    }

    protected virtual void LoseTarget()
    {
        tran_Target = null;                                                     //타겟을 null로 변경
    }

    protected virtual void AttackTarget()
    {
        isAttack = true;                                                        //추후 랜덤함수 로 스킬 랜덤하게 나가게 구현                
    }


    /// <summary>
    /// enemy 가 target에게 데미지 주는 함수
    /// </summary>
    /// <returns></returns>
    protected virtual float Attack_Enemy()
    {
        return AttackPoint;
    }

    /// <summary>
    /// 맞았을떄 데미지 및 애니메이션 처리 함수 
    /// </summary>
    protected virtual void Hit_Enemy()
    {
        isHit = true;
        isAttack = false;
        float pAttackPoint = player.attackPoint;                                //플레이어 공격점수 가져오기 
        float skillPower = pSkillPoint * pAttackPoint;                          //맞은 스킬의 기술점수 * 공격점수 = 기술 힘 
        float damage = skillPower - (defencePoint * 0.3f);                      //데미지 = 기술힘 - 방어점수의30%

        HP -= (damage > 0) ? damage : 1.0f;                                     //데미지 최소값 확보

        if (isLive)                                                             //죽었는데 계속 때리면 맞는 경우가 발생하여 
        {
            anim_Enemy.SetTrigger("isHit");                                     //맞는 에니메이션 트리거 발동

            if (currentHP <= 0)
            {
                anim_Enemy.SetTrigger("isDie");                                 //죽는 에니메이션 트리거 발동 
            }
        }
        Debug.Log($"{currentHP}");                                              //UI 붙으면 삭제 
    }

    /// <summary>
    /// 죽었을 떄 오브젝트 풀로 되돌리고 경험치 및 점수 주는 함수 
    /// </summary>
    protected virtual void Die_Enemy()
    {
        isLive = false;
        player.AddExp(exp);                                                     // player에 exp 추가
        gameObject.SetActive(false);                                            // Enemy 비활성화        
    }

    protected virtual float GetEnemyHP()
    {
        return HP;
    }

}
