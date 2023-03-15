using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : StateBase
{
    private bool isPlayerDead = false;

    public float MoveSpeed = 0.1f;
    public float JumpPower = 10.0f;
    public float jumpCount;

    SpriteRenderer spriteRenderer;
    PlayerInputAction inputActions;
    Animator anim;
    Rigidbody2D rigid;

    EnemyBase enemy;
        


    IEnumerator skill1Coroutine;

    Vector3 inputDir = Vector3.zero;
    
    public Vector2 inputVec;
    public GameObject skill1;               // 스킬1 등록
    public GameObject skill2;               // 스킬2 등록
    float skill1Interval = 1.5f;

    IEnumerator Skill1Coroutine()
    {
        while (true)
        {            
            yield return new WaitForSeconds(skill1Interval);      // 연사 간격만큼 대기
        }
    }


    float playerH;                          //키 입력 방향 우측:1, 좌측 :-1

    //---------------------------------------------------------------------------------------------------
    

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InitStat();

        skill1Coroutine = Skill1Coroutine();    // 코루틴 미리 만들어 놓기

        enemy = FindObjectOfType<EnemyBase>(); // 적 찾아오기 
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack1.performed += OnSkill1;
        inputActions.Player.Attack1.canceled += OffSkill1;
        inputActions.Player.Attack2.performed += OnSkill2;
        inputActions.Player.Attack3.performed += OnSkill3;
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Attack3.performed -= OnSkill3;
        inputActions.Player.Attack2.performed -= OnSkill2;
        inputActions.Player.Attack1.canceled += OffSkill1;
        inputActions.Player.Attack1.performed -= OnSkill1;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();

        inputDir = dir;
    }

    private void OnSkill1(InputAction.CallbackContext context)   // 키보드 A키
    {
        //anim.SetTrigger("Skill1");
        GameObject obj = Instantiate(skill1);                   //skills 생성        
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if (playerH >= 0)
        {
            skill2.transform.localScale = new Vector3(1, 1, 0);       //우 누르면 우측에 생성

                                                                      
        }
        else if (playerH < 0)
        {
            skill2.transform.localScale = new Vector3(-1, 1, 0);    //좌 누르면 좌측에 생성 
        }
        Debug.Log($"{playerH}");
        obj.transform.position = new Vector3(x, y, 0);   //skills 생성위치
        //StartCoroutine(Skill1Coroutine());

    }

    private void OffSkill1(InputAction.CallbackContext context)   // 키보드 A키
    {
        //StopCoroutine(Skill1Coroutine());
    }

    private void OnSkill2(InputAction.CallbackContext context)  // 키보드 S키
    {
        GameObject obj = Instantiate(skill2);                   //skills 생성
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        
        if (playerH >= 0)
        {
            skill2.transform.localScale = new Vector3(1,1,0);       //우 누르면 우측에 생성             
        }
        else if(playerH < 0)
        {
            skill2.transform.localScale = new Vector3(-1, 1, 0);    //좌 누르면 좌측에 생성 
        }
        obj.transform.position = new Vector3(x, y + 0.5f, 0);          //skills 생성위치
                                                                       //Debug.Log("SkillS");
    }

    private void OnSkill3(InputAction.CallbackContext context)  // 키보드 D키
    {
        Debug.Log("SkillD");
    }

    private void FixedUpdate()  // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        playerH = Input.GetAxis("Horizontal");                  //키 입력 방향 우측:1, 좌측 :-1

        float d = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * d, ForceMode2D.Impulse);
        if (rigid.velocity.x > MoveSpeed)
        {
            rigid.velocity = new Vector2(MoveSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < MoveSpeed * (-1))
        {
            rigid.velocity = new Vector2(MoveSpeed * (-1), rigid.velocity.y);
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    jumpCount = 0;
                }
                anim.SetBool("Jump", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HP > 0)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyAttack"))         // Enemy와 충돌시 HP 감소
            {
                OnDamaged(collision.transform.position);   // 무적
            }             
        }        
        else if (HP < 0)// player 사망처리
        {
            isPlayerDead = true;
            PlayerDie();
        }
    }

    /// <summary>
    ///  무적 판정 처리 
    /// </summary>
    /// <param name="targetPos"></param>
    void OnDamaged(Vector2 targetPos)
    {
        HP -= enemy.EnemyAttack();

        //무적 처리 코드 
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : 0;
        rigid.AddForce(new Vector2(dirc,1),ForceMode2D.Impulse);
        Invoke("OffDamaged",3);
    }

    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 10);
    }

    protected int Level;

    protected float currentHp;                            //Hp 관련 (+ 프로퍼티)
    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            onHPChange?.Invoke(currentHp);
        }
    }

    protected int maxExp;                         //Exp 경험치 + (프로퍼티)
    protected int currentExp;
    public int EXP
    {
        get => currentExp;
        set
        {
            currentExp = value;
        }
    }
    int getExp;                                 //얻은 경험치

    

    //-----------------------------------------------------------------------------------------------------------
    // ----------- delegate-----------
    Action<float> onHPChange;
    // ---------------------------------

    private void Update()
    {
        if (Mathf.Abs(rigid.velocity.x) < 0.3)  // 애니메이션 
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
        }

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        
        transform.Translate(Time.deltaTime * MoveSpeed * inputDir);

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            rigid.AddForce(Vector2.up * JumpPower * 2, ForceMode2D.Impulse);
            jumpCount++;
            anim.SetBool("Jump", true);
        }

        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    // --------------함수시작-----------
    //초기스탯

    protected override void InitStat()
    {
        base.InitStat();
        EXP = 0;
        maxExp = 10;
        HP = maxHp = 100.0f;        
    }

    public void AddHP(float plus)
    {
        HP += plus;
        Debug.Log($"���� HP:{HP}");
    }

    public void AddExp(int plus)
    {
        EXP += plus;
        Debug.Log($"���� EXP:{currentExp}");
    }
    void LevelUp()                   // 레벨업
    {
        EXP -= maxExp;
        Level += 1;                  //레벨업시 어떻게 변화할지는 의논필요
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2;                //부드러운 경험치 bar를 위해 float으로 변경해야할지?        
        attackPoint *= 1.2f;
        defencePoint *= 1.2f;
        attackSpeed *= 1.2f;
    }

    //--------------delegate-----------------
    Action<int> onEXPChange;
    //---------------------------------------
    void GetEXP()
    {
        AddExp(getExp);
    }


    

    private void PlayerDie()
    {
        if (!isPlayerDead)
        {
            EXP = EXP - 50;   // player 사망시 경험치 감소
            gameObject.SetActive(false);
        }
    }

    protected void OnDamage()
    {
        
    }


}
