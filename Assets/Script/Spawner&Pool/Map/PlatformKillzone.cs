using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Device;

public class PlatformKillzone : MonoBehaviour
{
    public int platformCountEnd = 15;
    int platformCount;
    public Action<int> onPlatformCountChanged;
    public Action onStageEnd;

    private void Start()
    {
        platformCount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && collision.gameObject.GetComponent<LandBase>() != null)
        {
            collision.gameObject.SetActive(false);        
            platformCount++;
            Debug.Log(platformCount);
            onPlatformCountChanged?.Invoke(platformCount);

        }
        if (platformCount == platformCountEnd)
        {
            Debug.Log("Stage End");
            EditorApplication.isPaused = true;
            onStageEnd?.Invoke();
        }
    }

  
}
