using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : PlayerStat
{


    public int maxTargetCount = 5;

    protected Queue<GameObject> targetList;

    void Awake()
    {
        targetList = new Queue<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && targetList.Count < maxTargetCount && !targetList.Contains(gameObject))
        {
            Debug.Log($"트리거 안에 들어감 - 대상 트리거 : {collision.gameObject.name}");
            targetList.Enqueue(collision.gameObject);

        }
    }

  
}
