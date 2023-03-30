using System;
using System.Collections;
using UnityEngine;

public class StarSpawner : Spawner
{
    // 소환정도
    private void OnEnable()
    {
        interval = 4.0f;
    }
    protected override IEnumerator Spawn()
    {
        //Debug.Log(transform.position);

        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject obj = Factory.Inst.GetObject(PoolObjectType.ItemStar);   // 오브젝트 스포너위치에서 생성                                           //Debug.Log(obj.transform.position);
            ItemStar star = obj.GetComponent<ItemStar>();

            star.transform.position = transform.position;  // 스포너 위치로 이동
            float r = UnityEngine.Random.Range(minY, maxY);
            star.transform.Translate(Vector3.up * r);
        }
    }

}
