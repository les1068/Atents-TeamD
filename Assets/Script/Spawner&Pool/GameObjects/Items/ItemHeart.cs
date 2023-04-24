using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : ItemBase
{
    public float HPhealrate=0.5f;

    protected override void OnEnable()
    {
        itemExp = 20;
        itemScore = 40;
        base.OnEnable();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            base.OnTriggerEnter2D(collision);
            player.HP += player.maxHp * HPhealrate;
        }
    }
}
