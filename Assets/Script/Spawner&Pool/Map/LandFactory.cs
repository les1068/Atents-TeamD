using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType
{
    Land1 = 0,
    Land2,
    Land3,
    Land4,
    Land5
}
public class LandFactory : Singleton<LandFactory>
{
    Land1Pool land1pool;
    Land2Pool land2pool;
    Land3Pool land3pool;
    Land4Pool land4pool;
    Land5Pool land5pool;

    protected override void PreInitialize()
    {
        land1pool = GetComponentInChildren<Land1Pool>();
        land2pool = GetComponentInChildren<Land2Pool>();
        land3pool = GetComponentInChildren<Land3Pool>();
        land4pool = GetComponentInChildren<Land4Pool>();
        land5pool = GetComponentInChildren<Land5Pool>();
       
    }

    protected override void Initialize()
    {
        land1pool?.Initialize();
        land2pool?.Initialize();
        land3pool?.Initialize();
        land4pool?.Initialize();
        land5pool?.Initialize();
       
    }

    public GameObject GetObject(LandType type)
    {
        GameObject result = null;
        switch (type)
        {
            case LandType.Land1:
                result = GetLand1().gameObject;
                break;
            case LandType.Land2:
                result = GetLand2().gameObject;
                break;
            case LandType.Land3:
                result = GetLand3().gameObject;
                break;
            case LandType.Land4:
                result = GetLand4().gameObject;
                break;
            case LandType.Land5:
                result = GetLand5().gameObject;
                break;
        }
        return result;
    }


    public Land1 GetLand1() => land1pool?.GetObject();
    public Land2 GetLand2() => land2pool?.GetObject();
    public Land3 GetLand3() => land3pool?.GetObject();
    public Land4 GetLand4() => land4pool?.GetObject();
    public Land5 GetLand5() => land5pool?.GetObject();

}
