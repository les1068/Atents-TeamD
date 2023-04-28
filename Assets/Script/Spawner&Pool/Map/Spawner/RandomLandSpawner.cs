using System;
using System.Collections;
using UnityEngine;

public class RandomLandSpawner : LandSpawner
{
    int count = 0; // 킬존에서 소멸된 플랫폼 수를 받는 변수
    int targetCount = 0;
    int minusCount = 5; // Exit 플랫폼 소환하기 종료전 플랫폼 갯수 => 종료시점-minus = Exit 플랫폼 생성시점


    PlatformKillzone killzone;

    private void onEnable()
    {
        killzone = FindObjectOfType<PlatformKillzone>();
        targetCount = killzone.platformCountEnd - minusCount;    //플랫폼 종료될플랫폼수-minus(5개) = Exit 플랫폼 소환할 count
        killzone.onPlatformChanged += SetReady;
    }

    void SetReady(int killedPlatform)
    {
        count = killedPlatform;
        Debug.Log("count");
        if (targetCount == count)   //platformCountEnd-minus 시점과 count 수가 같으면 land5 소환
        {
            SpawnExitPlatform();
        }

    }
    protected override IEnumerator Spawn()
    {
        while (true)
        {
            /*if (targetCount == count)   //platformCountEnd-minus 시점과 count 수가 같으면 land5 소환
            {
                SpawnExitPlatform();
            }
*/
            yield return new WaitForSeconds(interval);
            
            GameObject obj = LandFactory.Inst.GetObject(RandomLandPool.LandPicker());   // 랜덤오브젝트 스포너위치에서 생성              
            OnSpawn(obj);
            
        }
    }
    protected override void OnSpawn(GameObject obj)
    {
        obj.transform.position = transform.position;
        float r = UnityEngine.Random.Range(minY, maxY);
        obj.transform.Translate(Vector3.up * r);      // 랜덤하게 높이 적용하기

    }
     void SpawnExitPlatform()
     {
             GameObject exitPlatform = LandFactory.Inst.GetObject(LandType.Land5);
             exitPlatform.transform.position = transform.position;
             float r = UnityEngine.Random.value;
             exitPlatform.transform.Translate(Vector3.up * r);      // 랜덤하게 높이 적용하기
    }

    private void OnDrawGizmos()
    {
        //스폰위치 확인용
        Gizmos.color = Color.yellow;
        Vector3 from = transform.position + Vector3.up * maxY;
        Vector3 to = transform.position + Vector3.up * minY;
        Gizmos.DrawLine(from, to);
    }

}
