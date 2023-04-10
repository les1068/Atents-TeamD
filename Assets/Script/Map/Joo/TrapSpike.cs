using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : TrapBase
{
    public float spikeDamage;

    protected override void OnEnable()
    {
        damage = 5.0f;
        base.OnEnable();
    }
}
