using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    public float JumpPower = 10.0f;
    public float jumpCount;
    SpriteRenderer spriteRenderer;

    PlayerInputAction inputActions;

    Vector3 inputDir = Vector3.zero;

    public Vector2 inputVec;

    Rigidbody2D rigid;

    public GameObject skills;               // 스킬1 등록

    float h;                                //키 입력 방향 우측:1, 좌측 :-1

    //---------------------------------------------------------------------------------------------------
    byte level;

    public byte Level
    {
        get => level;
        set
        {
            level = value;

        }
    }
    
    float currentHp; //Hp 관련 (+ 프로퍼티)

    float maxHp;

    public float HP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            onHPChange?.Invoke(currentHp);
        }
    }
   
    protected float attack; //attack 공격력
   
    float moveSpeed;  //moveSpeed 이동속도
    
    float attackSpeed; //attackSpeed 공격속도
  
    private int maxExp;   //Exp 경험치 + (프로퍼티)
    private int currentExp;
    public int EXP
    {
        get => currentExp;
        set
        {
            currentExp = value;

        }
    }

    int getExp;     //얻은 경험치

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;
        inputActions.Player.Attack1.performed += OnSkills;
        inputActions.Player.Attack2.performed += OnSkilld;
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Attack2.performed -= OnSkilld;
        inputActions.Player.Attack1.performed -= OnSkills;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();

        inputDir = dir;
    }

    private void OnAttack(InputAction.CallbackContext context)   // 키보드 A키
    {              
        //Debug.Log("Attack");
    }

    private void OnSkills(InputAction.CallbackContext context)  // 키보드 S키
    {
        GameObject obj = Instantiate(skills);                   //skills 생성
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        obj.transform.position = new Vector3(x, y, 0);          //skills 생성위치
        if (h >= 0)
        {
            skills.transform.localScale = new Vector3(1,1,0);       //우 누르면 우측에 생성             
        }
        else if(h < 0)
        {
            skills.transform.localScale = new Vector3(-1, 1, 0);    //좌 누르면 좌측에 생성 
        }
            //Debug.Log("SkillS");
    }
    private void OnSkilld(InputAction.CallbackContext context)  // 키보드 D키
    {
        Debug.Log("SkillD");
    }
   
    private void FixedUpdate()  // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        h = Input.GetAxis("Horizontal");                  //키 입력 방향 우측:1, 좌측 :-1

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            OnDamaged(collision.transform.position);
        //Debug.Log("피격");
        
    }
    void OnDamaged(Vector2 targetPos)
    {
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



    //-----------------------------------------------------------------------------------------------------------
    // ----------- delegate-----------
    Action<float> onHPChange;
    // ---------------------------------

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
        transform.Translate(Time.deltaTime * MoveSpeed * inputDir);

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            rigid.AddForce(Vector2.up * JumpPower * 2, ForceMode2D.Impulse);
            jumpCount++;
        }
        if (currentExp >= maxExp)
        {
            LevelUp();
        }

        transform.Translate(Time.deltaTime * MoveSpeed * inputDir);
    }

    // --------------함수시작-----------
    //초기스탯
    void InitStat()
    {
        level = 1;
        EXP = 0;
        maxExp = 10;
        HP = maxHp;
        moveSpeed = 10.0f;
        attack = 1;
        attackSpeed = 2.0f;
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
    void LevelUp() // 레벨업
    {
        EXP -= maxExp;
        Level += 1;
        //레벨업시 어떻게 변화할지는 의논필요
        maxHp *= 1.2f;
        HP = maxHp;
        maxExp *= 2; //부드러운 경험치 bar를 위해 float으로 변경해야할지?
        moveSpeed *= 1.2f;
        attack *= 1.2f;
        attackSpeed *= 1.2f;
    }


    //--------------delegate-----------------
    Action<int> onEXPChange;
    //---------------------------------------
    void GetEXP()
    {
        AddExp(getExp);
    }
}
