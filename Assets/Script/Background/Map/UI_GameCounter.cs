using System;
using System.Collections;
using UnityEngine;

public class UI_GameCounter : MonoBehaviour
{
    CanvasGroup group;
    Transform one;
    Transform two;
    Transform three;

    WaitForSeconds waitTime = new WaitForSeconds(1.0f);

    public Action StartRun;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();

        one = transform.GetChild(0).GetChild(0);
        two = transform.GetChild(0).GetChild(1);
        three = transform.GetChild(0).GetChild(2);
        // 숫자 비활성화 해놓기
        one.gameObject.SetActive(false);    
        two.gameObject.SetActive(false);       
        three.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        group.alpha = 1.0f;
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        three.gameObject.SetActive(true);
        yield return waitTime;
        three.gameObject.SetActive(false);
        two.gameObject.SetActive(true);
        yield return waitTime;
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(true);
        yield return waitTime;
        one.gameObject.SetActive(false);
        group.alpha = 0.0f;
        StartRun?.Invoke();
    }
}
