using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill2 : MonoBehaviour
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
    public float skillCoolTime = 1.0f;
    public int skillComboMax = 3;
    private int skillCombo;
    public int SkillCombo
    {
        get
        {
            return skillCombo;
        }
        set
        {
            skillCombo = Mathf.Clamp(value, 0, skillComboMax);
        }
    }
    bool isOnSkill = false;

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
        SkillCombo = 0;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Attack2.performed += OnSkill2;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack2.performed -= OnSkill2;
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

    public void OnSkill2(InputAction.CallbackContext context)                   // 키보드 S키
    {
        if (!isOnSkill)
        {
            StartCoroutine(IEOnSkill());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))    
        {
            //Debug.Log("skill2");
        }
    }
    
    private void Update()
    {
    }

    IEnumerator IEOnSkill()
    {
        SkillCombo++;
     
        isOnSkill = true;
        anim_Skill.SetTrigger("attack");
        if (SkillCombo == skillComboMax)
        {
            yield return new WaitForSeconds(skillCoolTime);
            StopCoroutine(IEOnSkill());
            SkillCombo = 0;
     
        }
        isOnSkill = false;
     
    }
}