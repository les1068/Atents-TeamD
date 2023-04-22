using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBase : ItemBase
{
    protected int coinscore;
    protected int coinexp;


    protected override void OnEnable()
    {
        itemScore = coinscore;
        itemExp = coinexp;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            player.AddExp(coinexp);
=======
            player.AddExp(coinExp);
            player.AddScore(coinScore);
            //Debug.Log($"{coinExp}{coinScore}");
>>>>>>> Stashed changes
            StartCoroutine(LifeOver());
        }
    }
}
