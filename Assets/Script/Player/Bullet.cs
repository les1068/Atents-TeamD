using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    Player player;
    Transform transBullet;
    Rigidbody2D rigidBullet;
   
    public float speed = 1.0f;
    /// <summary>
    /// 피격효과 effect 종류
    /// </summary>
    public PoolObjectType HitEffectprefab;
    
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
            //스킬맞았을 때 이펙트발동
            OnHitEffect(collision);
            //Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
            Destroy(gameObject, 0.5f);
            
        }
    }

    private void OnHitEffect(Collision2D collision)  // 스킬 맞았을 때 이펙트
    {
        GameObject obj = Factory.Inst.GetObject(PoolObjectType.Hit);
        obj.transform.position = collision.contacts[0].point;

    }

}
