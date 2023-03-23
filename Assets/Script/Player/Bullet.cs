using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    Skill3 skill3;
    Transform tran_Skill_Bullet;
    Rigidbody2D rigi_Skill_Bullet;
    public float speed = 10.0f;
    public float skillpoint = 1.0f;

  
    /// <summary>
    /// 피격효과 effect 종류
    /// </summary>
    public PoolObjectType HitEffectprefab;
    
    private void Awake()
    {
        rigidBullet = GetComponent<Rigidbody2D>();
        transBullet = GetComponent<Transform>();
        
    }

    private void OnEnable()
    {
        if(!skill3.isLeft)
        {
            rigi_Skill_Bullet.velocity = speed * Vector3.right;
        }
        else
        {
            rigi_Skill_Bullet.velocity = speed * Vector3.left;
        }        
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Platform"))
        {                    
            //스킬맞았을 때 이펙트발동
            OnHitEffect(collision);
            //Debug.Log($"공격이 {collision.gameObject.name}과 충돌");
            gameObject.SetActive(false);
            //Destroy(gameObject, 0.5f);            
        }
    }

    private void OnHitEffect(Collision2D collision)  // 스킬 맞았을 때 이펙트
    {
        GameObject obj = Factory.Inst.GetObject(PoolObjectType.Hit);
        obj.transform.position = collision.contacts[0].point;
    }

}
