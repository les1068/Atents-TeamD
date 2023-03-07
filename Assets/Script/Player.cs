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
    private void FixedUpdate()  // ���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + inputVec);
        
        
        /*rigid.AddForce(inputVec);   // ���� �ֱ�

        rigid.velocity= inputVec;   // �ӵ� ����

        rigid.MovePosition(rigid.position + inputVec);  // ��ġ �̵�*/
    }
}
