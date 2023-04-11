using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    BoxBoxer=0,
    Batafire,
    Boxy
}
public class EnemyFactory : Singleton<EnemyFactory>
{
    BoxboxerPool boxboxerpool;
    BatafirePool batafirepool;
    BoxyPool boxypool;

    protected override void PreInitialize()
    {
        boxboxerpool = GetComponentInChildren<BoxboxerPool>();
        batafirepool= GetComponentInChildren<BatafirePool>();
        boxypool= GetComponentInChildren<BoxyPool>();
    }

    protected override void Initialize()
    {

        boxboxerpool?.Initialize();
        batafirepool?.Initialize();
        boxypool?.Initialize();
    }

    public GameObject GetObject(EnemyType type)
    {
        GameObject result = null;
        switch (type)
        {
            case EnemyType.BoxBoxer:
                result = GetBoxer().gameObject;
                break;
            case EnemyType.Batafire:
                result = GetBatafire().gameObject;
                break;
            case EnemyType.Boxy:
                result = GetBoxy().gameObject;
                break;
   
        }
        return result;
    }
    public Enemy_Boxboxer GetBoxer() => boxboxerpool?.GetObject();
    public Enemy_Batafire GetBatafire() => batafirepool?.GetObject();
    public Enemy_Boxy GetBoxy() => boxypool?.GetObject();

}
