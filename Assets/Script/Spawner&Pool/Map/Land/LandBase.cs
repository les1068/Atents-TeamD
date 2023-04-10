using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBase : PoolObject
{
    public float moveSpeed = 2;
    Player player;
    Transform currentTransform;
    Vector2 moveDir;
    Vector2 moveDelta;
    Transform targetTransform;
    Rigidbody2D rigid;


    private void Awake()
    {
        targetTransform = FindObjectOfType<Player>().transform;
        rigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }


    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed * Vector2.left);
    }

}
