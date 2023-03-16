using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PoolObjectType
{
    //Skill=0,
    Enemy,
    //Explosion,
    //Hit,
    //Trap,
    //Coin,
    //Item
}

public class Factory : Singleton<Factory>
{
    //SkillPool skillPool;
    EnemyPool enemyPool;
    //ExplosionEffectPool explosioneffectPool;
    //HitEffectPool hiteffectPool;
    //TrapPool trapPool;
    //CoinPool coinPool;
    //itemPool itemPool;

    protected override void PreInitialize()
    {
        //skillPool = GetComponentInChildren<SkillPool>();
        enemyPool = GetComponentInChildren<EnemyPool>();
        //explosioneffectPool = GetComponentInChildren<ExplosionEffectPool>();
        //hiteffectPool = GetComponentInChildren(explosioneffectPool);
        //trapPool= GetComponentInChildren(trapPool);
        //coinPool= GetComponentInChildren(coinPool);
        //itemPool= GetComponentInChildren(itemPool);
    }

    protected override void Initialize()
    {
        //skillPool?.Initialize();
        enemyPool?.Initialize();
        //explosioneffectPool?.Initialize();
        //skillPool?.Initialize();
        //skillPool?.Initialize();
        //skillPool?.Initialize();
    }
    public Enemy GetEnemy() => enemyPool?.GetObject();
}
