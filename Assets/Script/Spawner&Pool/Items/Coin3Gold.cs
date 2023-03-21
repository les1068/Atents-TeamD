using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin3Gold : ItemBase
{
    [Header("Coin_Gold")]   //금코인 관련 정보
    public GameObject gold;
    public int goldscore = 10;
    public int goldexp = 7;


    protected override void OnEnable()
    {
        itemScore = goldscore;
        itemExp = goldexp;
    }
}
  