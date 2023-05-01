using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller_Background1 : Scroller_Base
{
    float slotWidth = 19.68f;  // 이미지 한변의 길이

    protected override void Awake()
    {
        Slot_Width = slotWidth;
        base.Awake();
    }
}
