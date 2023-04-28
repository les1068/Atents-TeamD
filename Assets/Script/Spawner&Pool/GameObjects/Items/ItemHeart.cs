using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : ItemBase
{
    public float HPhealrate = 0.5f;
    protected override void OnEnable()
    {
        ItemExp = 20;
        ItemScore = 40;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            player.HP += player.maxHp * HPhealrate;
        }
            base.OnTriggerEnter2D(collision);
    }
}
