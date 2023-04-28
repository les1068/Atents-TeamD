using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBase : PoolObject
{
    public float moveSpeed = 4;
    Player player;
    Transform currentTransform;
    Vector2 moveDir;
    Vector2 moveDelta;
    Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }


    protected virtual void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed * Vector2.left);
    }

}
