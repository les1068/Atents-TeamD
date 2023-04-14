using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPool : TestBase
{
    public BulletPool pool1;
    public HitEffectPool pool2;
    public Coin1Pool pool3;
    public StarPool pool4;

    Transform[] spawnTransforms;

    private void Start()
    {
        spawnTransforms = new Transform[transform.childCount];
        for(int i=0; i<transform.childCount;i++)
        {
            spawnTransforms[i] = transform.GetChild(i);
        }
    }
    protected override void Test1(InputAction.CallbackContext _)
    {
        PoolObject obj = pool1.GetObject();
        obj.transform.position = spawnTransforms[0].position;
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        PoolObject obj = pool2.GetObject();
        obj.transform.position = spawnTransforms[1].position;
    }
    protected override void Test3(InputAction.CallbackContext _)
    {
        PoolObject obj = pool3.GetObject();
        obj.transform.position = spawnTransforms[2].position;
    }
    protected override void Test4(InputAction.CallbackContext _)
    {
        PoolObject obj = pool4.GetObject();
        obj.transform.position = spawnTransforms[3].position;
    }
}
    