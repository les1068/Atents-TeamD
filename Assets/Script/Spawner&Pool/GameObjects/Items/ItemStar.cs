using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemStar : ItemBase
{
    public int Score = 10;
    public int Exp = 20;

    protected override void OnEnable()
    {
        itemScore = Score;
        itemExp = Exp;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.OnInvincibleMode();
        }
        base.OnTriggerEnter2D(collision);
    }

}
