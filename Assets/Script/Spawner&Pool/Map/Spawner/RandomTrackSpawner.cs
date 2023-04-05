using System;
using System.Collections;
using UnityEngine;

public class RandomTrackSpawner : MapSpawner
{
    
    protected override IEnumerator Spawn()
    {
        //Debug.Log(transform.position);

        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject obj = MapFactory.Inst.GetObject(RandomTrackPool.TrackPicker()) ;   // 랜덤오브젝트 스포너위치에서 생성              
            obj.transform.position = transform.position;  // 스포너 위치로 이동
            float r = UnityEngine.Random.Range(minY, maxY);
            obj.transform.Translate(Vector3.up * r);
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
