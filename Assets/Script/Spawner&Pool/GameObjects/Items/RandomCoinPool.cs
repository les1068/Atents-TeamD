using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CoinBase))]

public class RandomCoinPool : MonoBehaviour
{


    public static PoolObjectType RandomCoin()
    {
        float Rate = Random.Range(0.0f, 1.0f);
        float goldRate = 0.1f;
        float silverRate = 0.3f;

        PoolObjectType result;
        if (Rate < goldRate)
        {
            result = PoolObjectType.CoinGold;
        }
        else if (Rate > goldRate && Rate < silverRate)
        {
            result = PoolObjectType.CoinSilver;
        }
        else result = PoolObjectType.CoinCopper;
        return result;
    }

    void CoinPicker(PoolObjectType type)
    {
        switch (type)
        {
            case PoolObjectType.CoinCopper:
                GetCopper();
                break;
            case PoolObjectType.CoinSilver:
                GetSilver();
                break;
            case PoolObjectType.CoinGold:
                GetGold();
                break;
            default:
                break;
        }
    }

    private static void GetCopper() => Factory.Inst.GetCoin1();
    private static void GetSilver() => Factory.Inst.GetCoin2();
    private static void GetGold() => Factory.Inst.GetCoin3();
}