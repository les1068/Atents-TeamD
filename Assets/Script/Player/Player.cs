using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : StateBase
{
    SpriteRenderer spriteRenderer;
    PlayerInputAction inputActions;
    Animator anim;
    //Animator animSkill1;
    //Animator animSkill2;
    Rigidbody2D rigid;

    Enemy_Boxboxer enemy;

    Pause pause;

    Vector3 inputDir = Vector3.zero;
    
    public Vector2 inputVec;
    protected bool isLeft = false;            //마지막 키 입력 방향 확인용

    // -------------------------------------연주 수정
    bool canFallDown = false;
    float dirY;
    CapsuleCollider2D playercollider;
    //-----------------------------------------

    float playerH;                          //키 입력 방향 우측:1, 좌측 :-1
    [Header("스킬관련-------------------------------------")]
    public GameObject skill1;               // 스킬1 등록
    public GameObject skill2;               // 스킬2 등록
    public GameObject skill3;               // 스킬3 등록

    private bool isPlayerDead = false;
    [Header("스탯관련-------------------------------------")]
    public float MoveSpeed = 0.1f;
    public float JumpPower = 10.0f;
    public float jumpCount;

    //---------------------------------------------------------------------------------------------------

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playercollider = GetComponent<CapsuleCollider2D>();
        InitStat();

        enemy = FindObjectOfType<Enemy_Boxboxer>(); // 적 찾아오기

        pause = FindObjectOfType<Pause>();
    }

    private void Start()
    {
        moveSpeed = MoveSpeed;

        //animSkill1 = skill1.GetComponent<Animator>();
        //animSkill2 = skill2.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack1.performed += OnSkill1;        
        inputActions.Player.Attack2.performed += OnSkill2;
        inputActions.Player.Attack3.performed += OnSkill3;
        inputActions.Player.esc.performed += OnESC;
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.esc.performed -= OnESC;
        inputActions.Player.Attack3.performed -= OnSkill3;
        inputActions.Player.Attack2.performed -= OnSkill2;        
        inputActions.Player.Attack1.performed -= OnSkill1;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        inputDir = dir;
        playerH = dir.x;
        dirY = dir.y;
        if (playerH > 0)                                            // 마지막 키 입력 방향 확인용 
        {
            isLeft = false;
        }
        if (playerH < 0)
        {
            isLeft = true;
        }
    }

    private void OnSkill1(InputAction.CallbackContext context)   // 키보드 A키
    {
        
    }

    private void OnSkill2(InputAction.CallbackContext context)      // 키보드 S키
    {

    }

    private void OnSkill3(InputAction.CallbackContext context)  // 키보드 D키
    {
        
    }

    private void OnESC(InputAction.CallbackContext context)
    {
        pause.OnPause();
    }

    private void FixedUpdate()  // 물리 연산 프레임마다 호출되는 생명주기 함수
    {        
        rigid.AddForce(Vector2.right * playerH, ForceMode2D.Impulse);
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
        // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        if (canFallDown && dirY < 0) // 아래로 내려가기
        {
            
            OnFallDown();
        }
    }
    /// <summary>ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    /// Land에서 떨어질 때 실행되는 함수
    /// </summary>
    private void OnFallDown()
    {
        anim.SetBool("Jump", false);
        anim.SetBool("Walking", false);
        StartCoroutine(Falling());
    }
    /// <summary>
    /// Land에서 내려갈때 플레이어의 Collider 활성화/비활성화
    /// </summary>
    /// <returns></returns>
    IEnumerator Falling()
    {
        playercollider.enabled = false;
        Debug.Log("collider.enabled = false");
        yield return new WaitForSeconds(0.3f);
        playercollider.enabled = true;
        canFallDown = false;
    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HP > 0)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyAttack"))         // Enemy와 충돌시 HP 감소
            {
                OnDamaged(collision.transform.position);   // 무적
            }
            else if (collision.gameObject.CompareTag("Platform"))    //부딪힌 태그가 Platform이면
            {
                if (collision.gameObject.GetComponent<LandBase>() != null ) //LandBase를 가졌다면,
                {
                    canFallDown = true;
                    //Debug.Log("canFallDown(true)");
                }
                else
                {
                    canFallDown = false;
                    //Debug.Log("canFallDown(false)");
                }
                canFallDown = false;
                jumpCount = 0;

            }
        }        
        else if (HP < 0)// player 사망처리
        {
            isPlayerDead = true;
            PlayerDie();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<LandBase>() != null)
        {
            canFallDown = false;
          
        }
    }

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

    
    /// <summary>
    /// -----------------------무적/데미지관련----------------------------
    /// <summary>
    ///  무적 판정 처리 
    /// </summary>
    /// <param name="targetPos">충돌 체크시 위치</param>
    void OnDamaged(Vector2 targetPos)
    {
        //HP -= enemy.EnemyAttack();
        
        OnInvincibleMode();
        float dirc = transform.position.x - targetPos.x > 0 ? 0 : 1;        
        rigid.AddForce(new Vector2(dirc,1) * 20, ForceMode2D.Impulse);
    }

    public void OnInvincibleMode()
    {   //무적 처리 코드

        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        Invoke("OffDamaged", 3);
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
            Debug.Log($"현재 HP:{HP}");
            if(HP<0)
            {
                PlayerDie();
            }
            else if(HP>maxHp)
            {
                currentHp = maxHp;
            }
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
            onEXPChange?.Invoke(currentExp);
            Debug.Log($"Current Exp:{currentExp}");
        }
    }
    int getExp;                                 //얻은 경험치

    

    //-----------------------------------------------------------------------------------------------------------
    // ----------- delegate-----------
    Action<float> onHPChange;
    // ---------------------------------

   
    ///초기스탯
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

    }

    public void AddExp(int plus)
    {
        EXP += plus;
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
