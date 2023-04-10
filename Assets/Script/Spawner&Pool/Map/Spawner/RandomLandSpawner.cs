using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RandomLandSpawner : LandSpawner
{

    PlatformKillzone killerzone;

    protected override IEnumerator Spawn()
    {
        //Debug.Log(transform.position);

        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject obj = LandFactory.Inst.GetObject(RandomLandPool.TrackPicker()) ;   // 랜덤오브젝트 스포너위치에서 생성              
            OnSpawn(obj);
        }
    }

    protected override void OnSpawn(GameObject obj)
    {
        base.OnSpawn(obj);
        
    }
      void SetStop()
    {

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
