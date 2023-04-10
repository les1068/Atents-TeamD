using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapItem_Mushroom_color : MapItem_Base
{
    public float HPheal = 30;
    public int exp = 3;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        player.AddHP(HPheal);
        player.AddExp(exp);

        base.OnCollisionEnter2D(collision);
    }
}
