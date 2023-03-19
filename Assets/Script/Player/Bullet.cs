using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;
    Transform transBullet;
    Rigidbody2D rigidBullet;
    public float speed = 1.0f;
    
    private void Awake()
    {
        rigidBullet = GetComponent<Rigidbody2D>();
        transBullet = GetComponent<Transform>();
        
    }

    private void Start()
    {
        rigidBullet.velocity = speed * Vector3.right;
        
        Destroy(gameObject, 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Platform"))
        {
            //Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
            Destroy(gameObject, 0.5f);
            
        }
    }

}
