using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    public float Speed = 2.0f;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()  // 물리 연산 프레임마다 호출되는 생명주기 함수
    {
        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + inputVec);
        
        
        /*rigid.AddForce(inputVec);   // 힘을 주기

        rigid.velocity= inputVec;   // 속도 제어

        rigid.MovePosition(rigid.position + inputVec);  // 위치 이동*/
    }
}
