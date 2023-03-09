using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Radar
{
    /// <summary>
    /// 스킬 처리용 프로퍼티
    /// </summary>
    private float skillPower
    {
        
        get => skillPower;
        set
        {
            skillPower = Attack * value;
                
        }
    }

    
    




}
