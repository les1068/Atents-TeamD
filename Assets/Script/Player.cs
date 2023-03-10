using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float playMoveSpeed = 0.1f;
    public float jumpPower = 10.0f;
    public float jumpCount;
    public Vector2 inputVec;

    PlayerInputAction inputActions;
    Rigidbody2D rigid;

    Vector3 inputDir = Vector3.zero;


    private void Awake()
    {
        inputActions = new PlayerInputAction();
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
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Attack1.performed -= OnSkills;
        inputActions.Player.Attack2.performed -= OnSkilld;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();

        inputDir = dir;
    }

    private void OnAttack(InputAction.CallbackContext context)  // 키보드 A키
    {
        Debug.Log("Attack");
    }

    private void OnSkills(InputAction.CallbackContext context)  // 키보드 S키
    {
        Debug.Log("SkillS");
    }
    private void OnSkilld(InputAction.CallbackContext context)  // 키보드 D키
    {
        Debug.Log("SkillD");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        transform.Translate(Time.deltaTime * playMoveSpeed * inputDir);

        if(Input.GetKeyDown(KeyCode.UpArrow)&& jumpCount < 2)
        {
            rigid.AddForce(Vector2.up * jumpPower *2, ForceMode2D.Impulse);
            jumpCount++;
        }
       
    }
    private void FixedUpdate()  // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    jumpCount = 0;
                }
            }
        }
        /*rigid.AddForce(inputVec);   // 힘을 주기

        rigid.velocity= inputVec;   // 속도 제어

        rigid.MovePosition(rigid.position + inputVec);  // 위치 이동*/
    }
}
