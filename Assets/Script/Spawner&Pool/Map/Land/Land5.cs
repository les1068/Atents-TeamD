using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land5 : LandBase
{
    Vector2 pos;
    float elapsed= 0.0f;
    Scroller_Ground1 scroller;
    float scrollerSpeed;
    private void Start()
    {
        scroller = FindObjectOfType<Scroller_Ground1>();
        scrollerSpeed = scroller.scrollingSpeed;
    }
    protected override void FixedUpdate()
    {
        elapsed++;
        moveSpeed = scrollerSpeed * 1.5f;
        pos.x = transform.position.x;
        pos.y = Mathf.Cos(Time.fixedDeltaTime * elapsed);
        //transform.position = pos;
        transform.Translate(Time.fixedDeltaTime * moveSpeed * Vector2.left);
    }
}
