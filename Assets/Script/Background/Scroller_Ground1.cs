using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller_Ground1 : Scroller_Base
{
    float slotWidth = 21.70f;  // 그라운드 한변의 길이
    Transform Obstacle1;
    Transform Obstacle2;
    protected override void Awake()
    {
        Slot_Width = slotWidth;
        base.Awake();
    }

    protected override void MoveRightEnd(int index)
    {
        Transform child = transform.GetChild(0);
        Obstacle1 = child.GetChild(0);
        Obstacle2 = child.GetChild(1);

        for (int i = 0; i < transform.childCount; i++)
        {
            bgSlots[i].transform.GetChild(i).gameObject.SetActive(true);
            Obstacle1.gameObject.SetActive(true);
            Obstacle2.gameObject.SetActive(true);
        }
        base.MoveRightEnd(index);
    }


}
