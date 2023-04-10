using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCactus : TrapBase
{

    protected override void OnEnable()
    {
        damage = 5.0f;
        base.OnEnable();
    }
}
