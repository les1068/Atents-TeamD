using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    protected Player player;
    //스폰지점 확인용 변수 where(Gizmos)
    Vector3 where;
    public EnemyType enemyType;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        StartCoroutine(Spawn());
    }

    public float minInterval=3.0f;
    public float maxInterval=8.0f;

    float interval;

    protected virtual IEnumerator Spawn()
    {
        //Debug.Log(transform.position);

        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject obj = EnemyFactory.Inst.GetObject(enemyType); // 오브젝트 스폰
            // 상속 받은 클래스별 별도 처리
            OnSpawn(obj);
        }
    }
    /// <summary>
    /// 스폰하는 위치 변경시 사용할 함수
    /// </summary>
    /// <param name="type"></param>
    protected virtual void OnSpawn(GameObject obj)
    {
        interval = Random.Range(minInterval, maxInterval);
        obj.transform.position = this.transform.position;
        where = obj.transform.position;                       //스폰지점 확인용 (Gizmos)
    }

    private void OnDrawGizmos()
    {
        //스폰위치 확인용
          Gizmos.color = Color.green;
          Gizmos.DrawWireSphere(where,1.0f);
        
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
