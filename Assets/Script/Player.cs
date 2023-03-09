using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 0.1f;
    public float JumpPower = 10.0f;
    public float jumpCount;
    PlayerInputAction inputActions;
    Vector3 inputDir = Vector3.zero;
    public Vector2 inputVec;

    Rigidbody2D rigid;

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

    private void OnAttack(InputAction.CallbackContext context)  // Ű���� AŰ
    {
        Debug.Log("Attack");
    }

    private void OnSkills(InputAction.CallbackContext context)  // Ű���� SŰ
    {
        Debug.Log("SkillS");
    }
    private void OnSkilld(InputAction.CallbackContext context)  // Ű���� DŰ
    {
        Debug.Log("SkillD");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
        transform.Translate(Time.deltaTime * MoveSpeed * inputDir);

        if(Input.GetKeyDown(KeyCode.UpArrow)&& jumpCount < 2)
        {
            rigid.AddForce(Vector2.up * JumpPower *2, ForceMode2D.Impulse);
            jumpCount++;
        }
       
    }
    private void FixedUpdate()  // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
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
        /*rigid.AddForce(inputVec);   // ���� �ֱ�

        rigid.velocity= inputVec;   // �ӵ� ����

        rigid.MovePosition(rigid.position + inputVec);  // ��ġ �̵�*/
    }
}
