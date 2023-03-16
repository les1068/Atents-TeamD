using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int maxTargetCount = 5;

    protected Queue<GameObject> targetList;

    void Awake()
    {
        targetList = new Queue<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && targetList.Count < maxTargetCount)
        {
            //Debug.Log($"큐에 들어감 - 대상 트리거 : {collision.gameObject.name}");
            targetList.Enqueue(collision.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && targetList.Count < maxTargetCount)
        {
            //Debug.Log($"큐에서 나감 - 대상 트리거 : {collision.gameObject.name}");
            targetList.Dequeue();
            
        }
    }

}
