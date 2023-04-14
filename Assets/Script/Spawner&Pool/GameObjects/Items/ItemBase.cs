using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemBase : PoolObject 
{

    Animator anim;
    float moveSpeed = 4;


    int itemscore;  //점수
    protected int itemScore //점수프로퍼티
    {
        private get => itemscore;
        set
        {
            itemscore = value;
        }
    }
    int itemexp; //경험치
    protected int itemExp //경험치 프로퍼티
    {
        private get => itemexp;
        set
        {
            itemexp = value;
        }
    }
    float frequency; //빈도수
    protected Player player;
    

    protected virtual void Awake()
    {
        anim= GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    protected virtual void OnEnable()
    {
        itemScore = 1;
        itemExp = 1;
    }

    protected virtual void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.left * moveSpeed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {  
            collision.gameObject.GetComponent<Player>().AddExp(itemExp);
            StartCoroutine(LifeOver());
        }
    }
    

}
