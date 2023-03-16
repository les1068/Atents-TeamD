using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    PlayerInputAction inputActions;
    Transform attacktransform;
    
    bool doingAttack;
    float attackrate = 2.0f;
    float attackangle = 60f;

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
        
        doingAttack = false;

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
        
    }

    IEnumerator AttackCo()
    {
        doingAttack = true;
        for (int i = 0; i > attackangle; i++)
        {
            attacktransform.rotation = Quaternion.Euler(0, 0, i);
        }

        
        yield return new WaitForSeconds(attackrate);
    }

    public void OnAttack(InputAction.CallbackContext context)   // 키보드 A키
    {
        Debug.Log("공격");
        if (!doingAttack)
        {
            StopCoroutine(AttackCo());
            StartCoroutine(AttackCo());
        }
             
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && doingAttack)
        {            
            Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
        }
        doingAttack = false;

    }

    

}