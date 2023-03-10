using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skill1 : Radar
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






}
