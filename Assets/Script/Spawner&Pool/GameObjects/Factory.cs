using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    None=-1,
    Bullet=0,
    Hit,
    ItemStar,
    CoinCopper,
    CoinSilver,
    CoinGold,
    ItemHeart
}
public class Factory : Singleton<Factory>
{

    BulletPool bulletpool;
    HitEffectPool hitpool;
    StarPool starpool;
    Coin1Pool coin1pool;
    Coin2Pool coin2pool;
    Coin3Pool coin3pool;
    ItemHeartPool heartpool;

    protected override void PreInitialize()
    {
        bulletpool = GetComponentInChildren<BulletPool>();
        hitpool= GetComponentInChildren<HitEffectPool>();
        starpool= GetComponentInChildren<StarPool>();
        coin1pool= GetComponentInChildren<Coin1Pool>();
        coin2pool = GetComponentInChildren<Coin2Pool>();
        coin3pool = GetComponentInChildren<Coin3Pool>();
        heartpool = GetComponentInChildren<ItemHeartPool>();
    }

    protected override void Initialize()
    {

        bulletpool?.Initialize();
        hitpool?.Initialize();
        starpool?.Initialize();
        coin1pool?.Initialize();
        coin2pool?.Initialize();
        coin3pool?.Initialize();
        heartpool?.Initialize();
    }

    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.None:
                break;
            case PoolObjectType.Bullet:
                result = GetBullet().gameObject;
                break;
            case PoolObjectType.Hit:
                result = GetHitEffect().gameObject;
                break;
            case PoolObjectType.ItemStar:
                result = GetStar().gameObject;
                break;
            case PoolObjectType.CoinCopper:
                result = GetCoin1().gameObject;
                break;
            case PoolObjectType.CoinSilver:
                result = GetCoin2().gameObject;
                break;
            case PoolObjectType.CoinGold:
                result = GetCoin3().gameObject;
                break;
            case PoolObjectType.ItemHeart:
                result = GetHeart().gameObject;
                break;
        }
        return result;
    }
    public Bullet GetBullet() => bulletpool?.GetObject();
    public Effect GetHitEffect() => hitpool?.GetObject();
    public ItemStar GetStar() => starpool?.GetObject();
    public Coin1Copper GetCoin1() => coin1pool?.GetObject();
    public Coin2Silver GetCoin2() => coin2pool?.GetObject();
    public Coin3Gold GetCoin3() => coin3pool?.GetObject();

    public ItemHeart GetHeart() => heartpool?.GetObject();

}
