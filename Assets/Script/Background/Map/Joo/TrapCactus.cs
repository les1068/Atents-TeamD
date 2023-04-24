using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCactus : TrapBase
{
    public float cactusDamage = 6.0f;
    protected override void OnEnable()
    {
        Damage = cactusDamage;
        base.OnEnable();
    }
}
