using System;
using System.Collections;
using UnityEngine;

public class Coin3Spawner : Spawner
{
    private void OnEnable()
    {
        interval = 0.5f;
    }
    protected override IEnumerator Spawn()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(interval);

            GameObject obj = Factory.Inst.GetObject(PoolObjectType.CoinGold);   // 오브젝트 스포너위치에서 생성                                           //Debug.Log(obj.transform.position);
            Coin3Gold coin = obj.GetComponent<Coin3Gold>();

            coin.transform.position = transform.position;  // 스포너 위치로 이동
            float r = UnityEngine.Random.Range(minY, maxY);
            coin.transform.Translate(Vector3.up * r);
        }
    }

    private void OnDrawGizmos()
    {
        //스폰위치 확인용
          Gizmos.color = Color.green;
          Vector3 from = transform.position+ Vector3.up * maxY;
          Vector3 to = transform.position+ Vector3.up * minY;
          Gizmos.DrawLine(from, to);
        
        ////스폰지점 확인용
        //Gizmos.color = Color.blue;
        //GameObject comp = FindObjectOfType<EnemyBase>().gameObject;
        //Gizmos.DrawWireSphere(where, 0.5f);


        //어떻게 가는지 확인용
        //GameObject comp = FindObjectOfType<EnemyBase>().gameObject;
        //if (comp != null)
        //{
        //    Gizmos.color = Color.white;
        //    Gizmos.DrawLine(where, comp.transform.position);
        //}
        //
    }

}
