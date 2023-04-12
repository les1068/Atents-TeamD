using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    Transform[] waypoints;
    //웨이포인트의 번호 
    int index = 0;

    /// <summary>
    /// 현재 향하고 있는 웨이포인트의 트랜스폼 확인용 프로퍼티 
    /// </summary>
    public Transform CurruntWaypoint => waypoints[index];

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    /// <summary>
    /// 다음에 이동할 웨이포인트를 알려주는 함수 
    /// </summary>
    /// <returns>다음에 이동할 웨이포인트의 트렌스폼</returns>
    public Transform getNextWaypoint()
    {
        float yy = Random.Range(250f, 850f);                                    // 웨이포인트  y좌표 랜덤 설정 
        waypoints[index].position = new Vector3(waypoints[index].position.x, yy, 0);
        
        index++;
        index %= waypoints.Length;   //인덱스를 웨이포인츠 길이로 나누어 나머지값을 인덱스에 넣는다 .
        return waypoints[index];
    }
}
