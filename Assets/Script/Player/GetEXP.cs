using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExp : MonoBehaviour
{
    PlayerStat playerstat;

    //얻은 경험치
    int getExp;

    //--------------delegate-----------------
    Action<int> onEXPChange;
    //---------------------------------------
    void GetEXP()
    {
        playerstat?.AddExp(getExp);
    }
}
    
