using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    PlayerInputAction inputActions;
    Transform attacktransform;
    Animator anim;

    bool doingAttack;
    public float attackSpeedMulti = 1.0f;

    void AttackStart()
    {
        doingAttack = true;
    }
    void AttackEnd()
    {
        doingAttack = false;
    }
    
    /// <summary>
    /// 스킬 데미지 계산용 상수  
    /// </summary>
    public float skillValue = 1.0f;

    /// <summary>
    /// 스킬 데미지 계산용 프로퍼티 
    /// </summary>
    private float skillPower
    {
        get => skillPower;
        set
        {
            skillPower = value * skillValue;
        }
    }

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        Collider2D collider2D = GetComponentInChildren<Collider2D>();
        anim = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;        
    }

    public void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        anim.SetFloat("attackSpeed", attackSpeedMulti);
        
    }

    public void OnAttack(InputAction.CallbackContext context)   // 키보드 A키
    {
        anim.SetTrigger("Attack");
        //Vector2 dir = context.ReadValue<Vector2>();
        //anim.SetFloat("InputY", dir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && doingAttack)
        {
            Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
        }
    }
   
}