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
<<<<<<< Updated upstream
=======
            //player.AddScore(itemscore);
>>>>>>> Stashed changes
        }
    }
    int itemexp; //경험치
    protected int itemExp //경험치 프로퍼티
    {
        private get => itemexp;
        set
        {
            itemexp = value;
<<<<<<< Updated upstream
=======
            //player.AddExp(itemexp);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            collision.gameObject.GetComponent<Player>().AddExp(itemExp);
=======
            //collision.gameObject.GetComponent<Player>().AddExp(ItemExp);
            //collision.gameObject.GetComponent<Player>().AddExp(ItemScore);
>>>>>>> Stashed changes
            StartCoroutine(LifeOver());
        }
    }
    

}
