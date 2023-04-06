using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{    
    Button button_Back;
    RectTransform rectTransform;
    Transform tran_1;
    Transform tran_2;
    Transform tran_3;
    Transform tran_4;
    Vector3 randVec;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        tran_1 = rectTransform.GetChild(0);
        tran_2 = rectTransform.GetChild(1);
        tran_3 = rectTransform.GetChild(2);
        tran_4 = rectTransform.GetChild(3);

        button_Back = GameObject.Find("Back Button").GetComponent<Button>();
      
        button_Back.onClick.AddListener(OnBack);
    }

    private void OnBack()
    {
        SceneManager.LoadScene("MainLobby");
        //Debug.Log("OnBakc()");
    }

    private void Update()
    {
        randVec = new Vector3(Random.Range(-350.0f, 350.0f), Random.Range(-250.0f, 250.0f),0);
        
        tran_1.Translate(Time.deltaTime * randVec * 4);
        
        tran_2.Translate(Time.deltaTime * randVec * 3);
        
        tran_3.Translate(Time.deltaTime * randVec * 2);
        
        tran_4.Translate(Time.deltaTime * randVec * 1);
    }

}

