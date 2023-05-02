using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingScene_1 : Singleton<LoadingScene_1>
{
    float loadRatio = 0.0f;                         //로딩바의 value가 목표로 하는 값
    [Range(0.0f, 1.0f)]
    public float loadingBarSpeed = 0.5f;           //로딩바 증가 속도
    //public string nextSceneName = "TEST_ALL(Scrolling)";
    bool loadingComplete = false;

    int LoadingSceneCount=2;    

    PlayerInputAction inputAction;
    AsyncOperation async;           //비동기 명령 처리용
    Image image;                    // 로딩이미지
    TextMeshProUGUI tipText;         // 로딩중 팁 텍스트
    Slider slider;                  // 로딩바
    TextMeshProUGUI loadingText;    // 로딩 텍스트

    Image[] images;             // 랜덤으로 로딩 될 이미지들
    string[] tips;              // 랜덤으로 보여질 팁들
    string[] loadings;          // Loading 진행용 텍스트들



    private void Awake()
    {
        image = transform.GetChild(1).GetComponent<Image>();
        tipText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        slider = transform.GetChild(3).GetComponent<Slider>();
        loadingText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        inputAction = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputAction.UI.Enable();
        inputAction.UI.Click.performed += Press;
        inputAction.UI.AnyKey.performed += Press;
    }

    private void OnDisable()
    {
        inputAction.UI.AnyKey.performed -= Press;
        inputAction.UI.Click.performed -= Press;
        inputAction.UI.Disable();
    }
    private void Press(InputAction.CallbackContext _)
    {
        if(loadingComplete)
        {
            async.allowSceneActivation = true;    //씬 전환하기
        }
    }


    private void Start()
    {
        string tip1 = "커비는 큐트별에서 왔습니다.";
        string tip2 = "커비는 동물도 사람도 아닌, 무서운 캐릭터입니다.";
        string tip3 = "커비는 사람이 아닙니다. 그렇지만 귀엽습니다.";
        string tip4 = "세상에서 가장 귀여운 커비는 아직 누구도 만나지 못했습니다.";
        tips = new string[] { tip1, tip2, tip3, tip4 };

        //tips = new string[4];
        //tips = { tip1, tip2, tip3, tip4};
        //tips[0] = tip1;
        //tips[1] = tip2;
        //tips[2] = tip3;
        //tips[3] = tip4;
        int r = Random.Range(0, 3);
        slider.value = loadRatio;
        tipText.text = tips[r];
        loadings = new string[] { "Loading", "Loading . ", "Loading . .", "Loading . . ." };
        loadingText.text = loadings[0];
        loadingText.color = Color.white;


        
        StartCoroutine(LoadScene());
        StartCoroutine(LoadingText());

    }

    private void Update()
    {
        if(slider.value < loadRatio)
        {
            slider.value += Time.deltaTime * loadingBarSpeed;
        }
        else
        {
            slider.value = loadRatio;
        }
    }

    IEnumerator LoadScene()
    {      
        async = SceneManager.LoadSceneAsync(LoadingSceneCount);
      
        async.allowSceneActivation = false;

        while (loadRatio < 1.0f) 
        {
            loadRatio = async.progress + 0.1f;
            Debug.Log(async.progress);
            Debug.Log($"ratio :{loadRatio}");
            yield return null;
        }

        yield return new WaitForSeconds((loadRatio - slider.value) / loadingBarSpeed);

        loadingComplete = true;
        //색상 변경안됌!;
        loadingText.color = new Color(255, 250, 100);
        loadingText.text = "Let's Run!";
    }

    IEnumerator LoadingText()
    {
        int index = 0;
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while (slider.value < 1.0f)
        {
            loadingText.text = loadings[index];
            index++;
            index %= loadings.Length;
            yield return waitTime;
        }
    }
}
