using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBase : PoolObject
{
    public float moveSpeed = 1;
    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector2.left);
    }
}
