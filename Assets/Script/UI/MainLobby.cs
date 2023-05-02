using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class MainLobby : MonoBehaviour
{    
    // 캔버스의 버튼 외 
    Button button_GameStart;
    Button button_Credits;
    Button button_Exit;
    Image image;
    RectTransform recttransform;
    Transform tran_image;
    

    // 이미지 이동용 웨이포인트 외  
    public Waypoints targetWaypoints;
    public Action<Vector3> onMove;
    Transform target;

    Vector3 moveDelta = Vector3.zero;
    Vector3 moveDir;

    float moveSpeed = 500.0f;
    float targetDistance = 50.0f;


    private void Awake()
    {
        // 버튼 찾아오기 
        button_GameStart = GameObject.Find("GameStart Button").GetComponent<Button>();
        button_Credits = GameObject.Find("Credits Button").GetComponent<Button>();
        button_Exit = GameObject.Find("Exit Button").GetComponent<Button>();
        button_GameStart.onClick.AddListener(OnGameStart);
        button_Credits.onClick.AddListener(OnCredits);
        button_Exit.onClick.AddListener(OnExit);

        // 움직일것 찾아오기 
        image = GetComponentInChildren<Image>();
        recttransform = GetComponent<RectTransform>();
        tran_image = transform.GetChild(0).GetComponent<Transform>();
    }

    private void Start()
    {
      
        SetTarget(targetWaypoints.CurruntWaypoint);                             // 시작 이동할 , 현재 웨이포인트 찾기 
    }

    private void FixedUpdate()
    {
        Move();                                                                 // 이동용 무브 함수 실행 
    }

    private void OnGameStart()                                                  // 버튼용 함수 
    {        
        SceneManager.LoadScene(1);       
        //Debug.Log("OnGameStart()");
    }

    private void OnCredits()                                                    // 버튼용 함수 
    {        
        SceneManager.LoadScene(7);
        //Debug.Log("OnCredits()");
    }

    private void OnExit()                                                       // 버튼용 함수 
    {
        Application.Quit();                                                     // 빌드 후 작동 하는 코드 
        //Debug.Log("OnExit()");
    }

    private void Move()
    {
        moveDelta = Time.fixedDeltaTime * moveSpeed * moveDir;                  // 이동방향 및 속도 
        tran_image.localScale = (moveDir.x > 0) ? new Vector3(-1,1,1) : new Vector3(1,1,1); // 이동방향에 따른 이미지 반전용 
        tran_image.Translate(moveDelta, Space.World);                           // 이미지 이동 

        if ((target.position - tran_image.position).sqrMagnitude < targetDistance) // 거리가 targetDistance보다 작을 떄 
        {
            SetTarget(targetWaypoints.getNextWaypoint());                       // 다음 이동할 웨이포인트 설정 
            moveDelta = Vector3.zero;
            OnArrived();                                                        // 도착 시 발생 함수 
        }
        onMove?.Invoke(moveDelta);                                              
    }

    private void SetTarget(Transform target)
    {
        this.target = target;                                                   
        moveDir = (this.target.position - tran_image.position).normalized;      //이동 방향 설정 
        moveSpeed = Random.Range(300.0f, 500.0f);                               //이동 랜덤하게 설정 
    }

    private void OnArrived()
    {

    }

}
