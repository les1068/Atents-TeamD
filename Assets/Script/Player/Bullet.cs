using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    Skill3 skill3;
    Transform tran_Skill_Bullet;
    Rigidbody2D rigi_Skill_Bullet;
    public float speed = 7.5f;
    public float skillpoint = 1.0f;
    Vector3 rightVec;
    Vector3 leftVec;



    /// <summary>
    /// 피격효과 effect 종류
    /// </summary>
    public PoolObjectType HitEffectprefab;
    
    private void Awake()
    {
        rigi_Skill_Bullet = GetComponent<Rigidbody2D>();
        tran_Skill_Bullet = GetComponent<Transform>();
        skill3 = FindObjectOfType<Skill3>();
        rightVec = new Vector3(1, 1, 0);
        leftVec = new Vector3(-1, 1, 0);

    }

    private void OnEnable()
    {
        if(!skill3.IsLeft)
        {
            rigi_Skill_Bullet.velocity = speed * rightVec;
        }
        else
        {
            rigi_Skill_Bullet.velocity = speed * leftVec;
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
