using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Skill1 : MonoBehaviour
{    
    PlayerInputAction inputActions;
    Player player;
    Transform transSkill;
    Animator animSkill;
    Vector3 inputDir = Vector3.zero;
    EnemyBase enemy;

    //bool isLeft = false;
        
    /// <summary>
    /// 스킬 데미지 계산용 변수
    /// </summary>
    public float skillValue = 1.0f;

    /// <summary>
    /// 스킬 데미지 계산 후 변수 
    /// </summary>
    public float skillPower
    {
        get => skillPower;
        set
        {
            skillPower = value * skillValue * player.attackPoint;
        }
    }

    float enemy_DefencePoint;
    float enemy_currentHP;

    private void Awake()
    {        
        inputActions = new PlayerInputAction();
        animSkill = GetComponent<Animator>();
        enemy = FindObjectOfType<EnemyBase>(); // 적 찾아오기 
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Attack1.performed += OnSkill1;        
    }

    private void OnDisable()
    {        
        inputActions.Player.Attack1.performed -= OnSkill1;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        inputDir = dir;
        
        if (dir.x > 0)                                            // 마지막 이동 위치 확인용 
        {
            //isLeft = false;
            animSkill.SetBool("isLeft", false);
        }
        if (dir.x < 0)
        {
            //isLeft = true;
            animSkill.SetBool("isLeft", true);
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)   // 키보드 A키
    {        
        anim_Skill.SetTrigger("attack");        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(coll_Skill.enabled && collision.gameObject.CompareTag("Enemy"))      // 적이고 스킬 range의 coll이 enable이면 
        {                                                                       // range의 coll을 disable 시켜라
            coll_Skill.enabled = false;                                         // 생각대로 작동안함 ㅠ ㅠ             
        }        
    }

    private void Update()
    {
    }
}