using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBase : ItemBase
{
    protected int coinScore;
    protected int coinExp;

    protected override void OnEnable()
    {
        ItemScore = coinScore;
        ItemExp = coinExp;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.AddExp(coinExp);
            player.AddScore(coinScore);
            ItemEffect();
            //StartCoroutine(LifeOver());
        }
    }
}
