using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin1Copper : CoinBase
{
    [Header("Coin1_Copper")] //동코인 관련정보
    public GameObject copper;
    public int copperscore = 1;
    public int copperexp = 1;

    protected override void OnEnable()
    {
        coinscore = copperscore;
        coinexp = copperexp;
        base.OnEnable();
    } 
}
  