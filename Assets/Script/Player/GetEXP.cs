using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExp : MonoBehaviour
{
    PlayerStat playerstat;

    //���� ����ġ
    int getExp;

    //--------------delegate-----------------
    Action<int> onEXPChange;
    //---------------------------------------
    void GetEXP()
    {
        playerstat?.AddExp(getExp);
    }
}
    
