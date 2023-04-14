using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Emeny1 : MonoBehaviour
{
    Rigidbody2D rigid;
    public float TestEnemySpeed = 2.0f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(-TestEnemySpeed, rigid.velocity.y);
    }
}
