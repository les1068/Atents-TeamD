using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : TrapBase
{
    public float spikeDamage;

    protected override void OnEnable()
    {
        Damage = 5.0f;
        base.OnEnable();
    }
}
