using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land5 : LandBase
{
    Vector2 pos;
    float elapsed;

    protected override void FixedUpdate()
    {
        elapsed++;
        transform.Translate(Time.fixedDeltaTime * moveSpeed * Vector2.left);
        pos.x = transform.position.x;
        pos.y = Mathf.Cos(Time.fixedDeltaTime * elapsed);
        transform.position = pos;
    }
}
