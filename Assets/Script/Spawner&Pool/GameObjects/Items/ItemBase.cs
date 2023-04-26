using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemBase : PoolObject 
{
    protected Player player;
    Animator anim;
    float moveSpeed = 4;

    int itemscore;  //점수
    protected int ItemScore //점수프로퍼티
    {
        private get => itemscore;
        set
        {
            itemscore = value;
            //player.AddScore(itemscore);
        }
    }

    int itemexp; //경험치
    protected int ItemExp //경험치 프로퍼티
    {
        private get => itemexp;
        set
        {
            itemexp = value;
            //player.AddExp(itemexp);
        }
    }

    float frequency; //빈도수

    protected virtual void Awake()
    {
        anim= GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    protected virtual void OnEnable()
    {
        ItemScore = 1;
        ItemExp = 1;
    }

    protected virtual void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.left * moveSpeed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            transform.Translate(Vector2.up * 0.5f,Space.Self);
            transform.Rotate(transform.position,360f);
            
            //collision.gameObject.GetComponent<Player>().AddExp(ItemExp);
            //collision.gameObject.GetComponent<Player>().AddExp(ItemScore);
            StartCoroutine(LifeOver());
        }
    }
}
