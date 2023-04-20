using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin3Gold : CoinBase
{
    [Header("Coin_Gold")]   //금코인 관련 정보
    public GameObject gold;
    public int goldScore = 10;
    public int goldExp = 7;


    protected override void OnEnable()
    {
        coinScore = goldScore;
        coinExp = goldExp;

        base.OnEnable();
    }
}
  