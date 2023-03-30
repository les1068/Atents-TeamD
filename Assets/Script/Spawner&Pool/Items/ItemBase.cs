using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemBase : PoolObject 
{

    Animator anim;
    float moveSpeed = 2;


    int itemscore;  //점수
    protected int itemScore //점수프로퍼티
    {
        private get => itemscore;
        set
        {
            itemscore = value;
            //Debug.Log($"score : {itemscore}");
        }
    }
    int itemexp; //경험치
    protected int itemExp //경험치 프로퍼티
    {
        private get => itemexp;
        set
        {
            itemexp = value;
            //Debug.Log($"score : {itemexp}");
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
        itemscore = 1;
        itemexp = 1;
    }

    protected virtual void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.left * moveSpeed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {  
            player.AddExp(itemexp);
            StartCoroutine(LifeOver());
        }
    }
    

}
