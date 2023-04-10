using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    protected override IEnumerator Spawn()
    {

        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject obj = Factory.Inst.GetObject(objType);   // 오브젝트 스포너위치에서 생성                               
            Enemy_Boxboxer enemy = obj.GetComponent<Enemy_Boxboxer>();

            enemy.transform.position = transform.position;
            Debug.Log(enemy.transform.position);
            float r = UnityEngine.Random.Range(minY, maxY);
            // EnemyBase에 플레이어 설정
            enemy.transform.Translate(Vector3.up * r);

        }
    }
}
