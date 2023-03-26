using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPool_Map : TestBase
{
    public Track0Pool pool1;
/*    public Track1Pool pool2;
    public Track2Pool pool3;
    public Track3Pool pool4;
    public Track4Pool pool5;
*/
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
/*    protected override void Test2(InputAction.CallbackContext _)
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
    protected override void Test5(InputAction.CallbackContext _)
    {
        PoolObject obj = pool5.GetObject();
        obj.transform.position = spawnTransforms[4].position;
    }
*/}
    