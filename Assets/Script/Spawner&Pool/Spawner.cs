using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;//생성할 오브젝트 타입
    public float maxY = 4;  //생성할 최고위치
    public float minY = -4; //생성할 최저위치
    public float interval = 1.0f;//생성 시간 간격

    protected Player player = null;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);
            GameObject obj = Factory.Inst.gameObject;
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(1, Mathf.Abs(maxY) + Mathf.Abs(minY)+2, 1));
        
    }

}
