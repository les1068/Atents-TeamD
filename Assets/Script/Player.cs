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
        inputActions.Player.Disable();
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Attack1.performed -= OnSkills;
        inputActions.Player.Attack2.performed -= OnSkilld;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();

        inputDir = dir;
    }

    private void OnAttack(InputAction.CallbackContext context)   // Ű���� AŰ
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
        transform.Translate(Time.deltaTime * MoveSpeed * inputDir);
    }
    private void FixedUpdate()  // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
       
        rigid.MovePosition(rigid.position + inputVec);
        
        
        /*rigid.AddForce(inputVec);   // ���� �ֱ�

        rigid.velocity= inputVec;   // �ӵ� ����

        rigid.MovePosition(rigid.position + inputVec);  // ��ġ �̵�*/
    }
}
