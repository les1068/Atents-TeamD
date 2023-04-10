using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin2Silver : CoinBase
{
    [Header("Coin2_Silver")] //은코인 관련정보
    public GameObject silver;
    public int silverscore = 3;
    public int silverexp = 3;

    protected override void OnEnable()
    {
        coinscore = silverscore;
        coinexp = silverexp;
        base.OnEnable();
    }





}
  