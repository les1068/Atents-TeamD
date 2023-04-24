using System;
using System.Collections;
using UnityEngine;

public class LandSpawner : MonoBehaviour 
{
    protected Player player;
    protected PlatformKillzone killzone;
    public LandType objType;
    /// <summary>
    /// 스폰간격
    /// </summary>
    public float interval = 3.0f;
    /// <summary>
    /// 스폰되는 위치
    /// </summary>
    public float minY = -2.2f;
    public float maxY = 2.5f;
    
    //스폰지점 확인용 변수 where(Gizmos)
    Vector3 where;
    /// <summary>
    /// Killplatform수
    /// </summary>
    protected int count;
    /// <summary>
    /// 게임종료전 D-플랫폼갯수 (exitplatform소환)
    /// </summary>
    protected int beforeCount;
    /// <summary>
    /// Land5 스폰될 시점
    /// </summary>
    protected int spawnReady;

    protected bool isReady = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        killzone = GameObject.Find("PlatformKillZone").GetComponent<PlatformKillzone>();
        
        spawnReady = killzone.platformCountEnd - beforeCount;

        killzone.onPlatformCountChanged += (platform) => count = platform;

        StartCoroutine(Spawn());
    }
    protected virtual IEnumerator Spawn()
    {
        //Debug.Log(transform.position);

        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject obj = LandFactory.Inst.GetObject(objType);   // 오브젝트 스폰
            // 상속 받은 클래스별 별도 처리
            OnSpawn(obj);
        }
    }

    public void SpawnExitPlatform()
    {
        GameObject exitPlatform = LandFactory.Inst.GetObject(LandType.Land5);
        exitPlatform.transform.position = transform.position;
        float r = UnityEngine.Random.value;
        exitPlatform.transform.Translate(Vector3.up * r);      // 랜덤하게 높이 적용하기
        isReady = false;
    }

    /// <summary>
    /// 스폰하는 위치 변경시 사용할 함수
    /// </summary>
    /// <param name="type"></param>
    protected virtual void OnSpawn(GameObject obj) 
    {
        obj.transform.position = transform.position;
        float r = UnityEngine.Random.Range(minY, maxY);
        obj.transform.Translate(Vector3.up * r);      // 랜덤하게 높이 적용하기
        where = obj.transform.position;                       //스폰지점 확인용 (Gizmos)
    }

    private void OnDrawGizmos()
    {
        //스폰위치 확인용
          Gizmos.color = Color.yellow;
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
