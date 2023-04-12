using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCactus : TrapBase
{

    protected override void OnEnable()
    {
        Damage = 5.0f;
        base.OnEnable();
    }
}
