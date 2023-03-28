using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Skill1 : MonoBehaviour
{
    PlayerInputAction inputActions;
    Transform tran_Skill;
    Transform tran_SkillRange;
    Animator anim_Skill;
    Vector3 inputDir = Vector3.zero;
    Collider2D coll_Skill;
            
    /// <summary>
    /// 스킬 데미지 계산용 변수
    /// </summary>
    public float skillpoint = 1.0f;
    public float skillSpeed = 1.0f;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        anim_Skill = GetComponent<Animator>();
        tran_Skill = GetComponent<Transform>();
        tran_SkillRange = tran_Skill.GetChild(0);
        coll_Skill = tran_SkillRange.GetComponent<Collider2D>();
    }

    private void Start()
    {
        anim_Skill.SetFloat("SkillSpeed", skillSpeed);
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
        inputDir = dir;                                                         // 마지막 이동 위치 확인용 

        if (dir.x > 0)                                            
        {            
            anim_Skill.SetBool("isLeft", false);
        }
        if (dir.x < 0)
        {         
            anim_Skill.SetBool("isLeft", true);
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)                   // 키보드 A키
    {        
        anim_Skill.SetTrigger("attack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(coll_Skill.enabled && collision.gameObject.CompareTag("Enemy"))      // 적이고 스킬 range의 coll이 enable이면 
        {                                                                       // range의 coll을 disable 시켜라
            coll_Skill.enabled = false;                                         // 생각대로 작동안함 ㅠ ㅠ 
            //Debug.Log($"{coll_Skill.isActiveAndEnabled}");                      // 확인용 디버그 
        }        
    }
    
    private void Update()
    {
    }
}