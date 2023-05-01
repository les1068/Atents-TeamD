using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scroller_Base : MonoBehaviour
{
    
    UI_GameCounter gameCounter;
    LandSpawner landSpawner;
    protected PlatformKillzone killzone;

    public float scrollingSpeed = 4f;  // 스크롤 이동 속도

    int currentCount;  //죽은 플랫폼수
    int targetCount; // 히든 플랫폼 Land5 소환할 플랫폼수
    int minusCount = 3;

    protected Transform[] bgSlots = null;  // 배경 이미지가 두개 붙어있는 슬롯의 집합
    protected bool isStart = false;

    float slot_Width;  // 이미지 한변의 길이

    public float Slot_Width
    {
        get => slot_Width;
        set
        {
            slot_Width = value;
        }
    }


    protected virtual void Awake()
    {
        gameCounter = FindObjectOfType<UI_GameCounter>();
        killzone = FindObjectOfType<PlatformKillzone>();
        landSpawner = FindObjectOfType<RandomLandSpawner>();

        targetCount = killzone.platformCountEnd - minusCount;
        bgSlots = new Transform[transform.childCount];  // 슬롯이 들어갈 배열 생성
        for (int i = 0; i < transform.childCount; i++)
        {
            bgSlots[i] = transform.GetChild(i);         // 슬롯 하나씩 찾기
        }
        slot_Width = bgSlots[1].position.x - bgSlots[0].position.x;   // 이미지 한변의 길이 계산

    }
    private void Start()
    {
        killzone.onPlatformChanged += ((killedplat) =>
        {
            currentCount = killedplat;
           
            //Debug.Log($"targetCount : {targetCount} , platCount : {currentCount}");
            if(targetCount == currentCount)
            {
                Land5 hiddenPlatform =LandFactory.Inst.GetLand5();
                GameObject obj = hiddenPlatform.gameObject;
                obj.GetComponent<Land5>();
                obj.transform.position = landSpawner.transform.position;
            }

            
        });
        gameCounter.StartRun += OnStart;
    }

    void OnStart()
    {
        isStart = true;
    }

    private void Update()
    {
        if (isStart)             // 게임카운트 후에
        {
            for (int i = 0; i < bgSlots.Length; i++)  // 아래 foreach와 같은 코드. 하지만 foreach가 더 빠르다.
            {
                Transform slot = bgSlots[i];
                slot.Translate(Time.deltaTime * scrollingSpeed * -transform.right);
                if (slot.localPosition.x < 0)      // 슬롯이 부모 위치보다 왼쪽으로 갔을 때
                {

                    MoveRightEnd(i);  // 슬롯을 오른쪽 끝으로 이동 시키기
                }
            }
        }
    }

    protected virtual void MoveRightEnd(int index)
    {
        bgSlots[index].Translate(slot_Width * bgSlots.Length * transform.right);
    }
}
