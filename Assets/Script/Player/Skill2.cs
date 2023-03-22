using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill2 : MonoBehaviour
{
    PlayerInputAction inputActions;
    Player player;
    Transform transSkill;
    Animator animSkill;
    Vector3 inputDir = Vector3.zero;

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

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        animSkill = GetComponent<Animator>();
    }

    private void Start()
    {

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

    public void OnSkill2(InputAction.CallbackContext context)   // 키보드 A키
    {
        animSkill.SetTrigger("attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
        }
    }

    private void Update()
    {
    }




}