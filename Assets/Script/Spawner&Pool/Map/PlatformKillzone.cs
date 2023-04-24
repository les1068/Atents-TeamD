using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class PlatformKillzone : MonoBehaviour
{
    public int platformCountEnd = 10;
    int platformCount;
    public Action<int> onPlatformCountChanged;
    public Action onStageEnd;

    private void Start()
    {
        platformCount = 0;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && collision.GetComponent<LandBase>() != null)
        {
            collision.gameObject.SetActive(false);
            platformCount++;
            //Debug.Log(platformCount);
            onPlatformCountChanged?.Invoke(platformCount);

        }
        else if (collision.GetComponent<Bullet>() || collision.GetComponent<CoinBase>())
        {
            collision.gameObject.SetActive(false);
        }


       //게임 종료 조건 
        if (platformCount == platformCountEnd)
        {
            OnStageEnd();
        }
    }
    public void OnStageEnd()
    {
        StopAllCoroutines();
        Debug.Log("Stage End");
        EditorApplication.isPaused = true;          //플레이 일시정지됨
        onStageEnd?.Invoke();                       //끝났다고 알리는 델리게이트
    }
  
}
