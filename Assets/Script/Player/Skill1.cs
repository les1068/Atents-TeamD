using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill1 : MonoBehaviour
{
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

    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"스킬이 {collision.gameObject.name}과 충돌");
        }
    }

    


}