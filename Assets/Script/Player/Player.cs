using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : StateBase
{
    UI_GameCounter gameCounter;         //gameCounter(3,2,1 카운트)

    SpriteRenderer spriteRenderer;
    PlayerInputAction inputActions;
    Animator anim;
    Rigidbody2D rigid;

    Enemy_Batafire enemy_Batafire;
    Enemy_Boxboxer enemy_Boxboxer;
    Enemy_Boxy enemy_Boxy;
    BossAttack bossAttack;

    Pause pause;

    Vector3 inputDir = Vector3.zero;

    public Vector2 inputVec;
    protected bool isLeft = false;            //마지막 키 입력 방향 확인용

    // -------------------------------------연주 수정
    bool isStart = false;           //3,2,1 완료여부 :  true면 게임시작, false면 카운트중임
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

    float enemyattack;

    //---------------------------------------------------------------------------------------------------

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playercollider = GetComponent<CapsuleCollider2D>();
        InitStat();
        gameCounter = FindObjectOfType<UI_GameCounter>(); // gameCounter 찾기
        pause = FindObjectOfType<Pause>();
    }

    private void Start()
    {
        moveSpeed = MoveSpeed;
        if (gameCounter !=null)
        {
            gameCounter.StartRun = () => isStart = true;
        }
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "TEST_ALL(Scrolling)" || SceneManager.GetActiveScene().name == "Test_joo_map")
        {
            inputActions.Player.Disable();
            RunningMapInputOnEnable();
        }
        else
        {
            inputActions.Player.Enable();
            inputActions.Player.esc.performed += OnESC;
            inputActions.Player.Move.performed += OnMoveInput;
            inputActions.Player.Move.canceled += OnMoveInput;
            anim.SetInteger("IdleCount", 0);
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "TEST_ALL(Scrolling)" || SceneManager.GetActiveScene().name == "Test_joo_map")
        {
            RunningMapInputOnDisable();
        }
        else
        {
            inputActions.Player.Move.canceled -= OnMoveInput;
            inputActions.Player.Move.performed -= OnMoveInput;
            inputActions.Player.esc.performed -= OnESC;
            inputActions.Player.Disable();
        }
    }
    void RunningMapInputOnEnable()
    {   if (isStart)
        {
            inputActions.Player.Enable();
            inputActions.PlayerRun.Enable();
            inputActions.PlayerRun.Down.performed += OnDown;
        }
    }
    void RunningMapInputOnDisable()
    {
        inputActions.PlayerRun.Down.performed -= OnDown;
        inputActions.PlayerRun.Disable();
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
    private void OnDown(InputAction.CallbackContext obj)
    {
        if (canFallDown)
        {
            StartCoroutine(Falling());
        }
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
        if (collision.gameObject.layer == 6)                                    // 플레이어가 적과 충돌 시 
        {
            OnDamaged(collision.transform.position);                            // 무적
        }
        else if (collision.gameObject.CompareTag("Platform"))                   //부딪힌 태그가 Platform이면
        {
            if (collision.gameObject.GetComponent<LandBase>() != null)         //LandBase를 가졌다면,
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<LandBase>() != null)
        {
            canFallDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))                 // 플레이어가 적에게 공격 당할 시 
        {
            if (collision.transform.parent.CompareTag("Enemy_BoxBoxer"))              // 적이 무엇인지 태그로 확인하여 해당 스크립트의 공격력을 enemyattack에 대입 
            {
                enemy_Boxboxer = collision.transform.GetComponentInParent<Enemy_Boxboxer>();
                enemyattack = enemy_Boxboxer.AttackPoint;
            }
            else if (collision.transform.parent.CompareTag("Enemy_Batafire"))              // 적이 무엇인지 태그로 확인하여 해당 스크립트의 공격력을 enemyattack에 대입 
            {
                enemy_Batafire = collision.transform.GetComponentInParent<Enemy_Batafire>();
                enemyattack = enemy_Batafire.AttackPoint;
            }
            else if (collision.transform.parent.CompareTag("Enemy_Boxy"))              // 적이 무엇인지 태그로 확인하여 해당 스크립트의 공격력을 enemyattack에 대입 
            {
                enemy_Boxy = collision.transform.GetComponentInParent<Enemy_Boxy>();
                enemyattack = enemy_Boxy.attackPoint;
                //enemyattack = enemy_Boxy.attackPoint;
            }
            
            OnDamage(enemyattack);                                              // 대미지 처리 함수            
        }
        else if(collision.transform.parent.CompareTag("BossAttack"))
            {
            bossAttack = collision.transform.GetComponentInParent<BossAttack>();
            //enemyattack = bossAttack.AttackPoint; --------------------------------------------------- 오류/주석처리함
            enemyattack = bossAttack.attackPoint;
            Debug.Log("b");
            OnDamage(enemyattack);                                              // 대미지 처리 함수            
        }
    }

    private void Update()
    {
        if (isStart)
        {
            anim.SetInteger("IdleCount", 1);
        }
        if (SceneManager.GetActiveScene().name == "TEST_ALL(Scrolling)" || SceneManager.GetActiveScene().name == "Test_joo_map")
        {
            anim.SetInteger("IdleCount", 1);
        }
        else
        {
            anim.SetInteger("IdleCount", 0);
        }

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
        HP -= 1.0f;

        OnInvincibleMode();
        float dirc = transform.position.x - targetPos.x > 0 ? 1 : 0;
        rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
    }

    public void OnInvincibleMode()
    {   //무적 처리 코드

        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 10);
    }

    ///초기스탯
    protected override void InitStat()
    {
        base.InitStat();
        EXP = 0;
        maxExp = 20;
        HP = maxHp;
    }

    public Action<float> onHPChange;
    protected float currentHp;                            //Hp 관련 (+ 프로퍼티)
    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            //Debug.Log($"현재 HP:{HP}");
            if (HP < 0)
            {
                isPlayerDead = true;
                PlayerDie();
            }
            else if (HP > maxHp)
            {
                currentHp = maxHp;
            }
            onHPChange?.Invoke(currentHp);
        }
    }

    public void AddHP(float plus)
    {
        HP += plus;
    }

    public Action<int> onEXPChange;
    protected int maxExp;                         //Exp 경험치 + (프로퍼티)
    protected int currentExp;
    public int EXP
    {
        get => currentExp;
        set
        {
            currentExp = value;

            onEXPChange?.Invoke(currentExp);
        }
    }

    public void AddExp(int plus)
    {
        EXP += plus;
    }



    public Action<int> onScoreChange;
    int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            onScoreChange?.Invoke(Score);
        }
    }

    public void AddScore(int plus)
    {
        Score += plus;
    }

    void LevelUp()                   // 레벨업
    {
        EXP -= maxExp;
        level += 1;                  //레벨업시 어떻게 변화할지는 의논필요
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2;                //부드러운 경험치 bar를 위해 float으로 변경해야할지?        
        attackPoint *= 1.2f;
        defencePoint *= 1.2f;
        attackSpeed *= 1.2f;
        pause.OnLeveUp();
    }
    public Action<float> ondamage;
    protected void OnDamage(float enemyattack)
    {
        float damage = enemyattack - (defencePoint * 0.3f);                 //데미지 = 적 공격력 - 방어점수의30%
        if (damage > 0)
        {
            AddHP(-damage);
        }
        else
        {
            AddHP(-1);
        }
        OnDamageText();
        ondamage?.Invoke(damage);
        //Debug.Log($"Player HP : {HP} : {damage} = {enemyattack} - {defencePoint} * 0.3f ");        
    }

    void OnDamageText()
    {
        GameObject obj = Factory.Inst.GetObject(PoolObjectType.DamageText);
        obj.transform.position = this.transform.position;
    }

    private void PlayerDie()
    {
        if (!isPlayerDead)
        {
            AddExp(-50);
            pause.OnPause();
        }
    }


}
