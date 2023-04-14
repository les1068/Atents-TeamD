using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Emeny2 : MonoBehaviour
{
    public float TestEnemySpeed = 2.0f;
    float baseY;
    float dir = 2.0f;     
    public float jumpPower = 2.0f;
    public float height = 3.0f;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()   // 물리연산
    {
        transform.Translate(Time.deltaTime * TestEnemySpeed * -transform.right);     //  무조건 왼쪽으로 이동
        transform.Translate(Time.deltaTime * TestEnemySpeed * jumpPower * transform.up);   // enemy 점프하는거

        if ((transform.position.y > baseY + height) || (transform.position.y < baseY - height))
        {
            dir *= -1.0f;       // dir = dir * -1;
        }
    }
}
