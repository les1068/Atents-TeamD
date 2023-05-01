using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    Player player;
    TextMeshProUGUI damagetext;
    RectTransform rect_DamageText;

    Color alpha;

    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;    
    float viewdamage;

    private void Awake()
    {
        rect_DamageText = GetComponentInChildren<RectTransform>();
        damagetext = rect_DamageText.GetComponentInChildren<TextMeshProUGUI>();

        alpha = damagetext.color;

        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();       
        //player.onDamageView += ViewDamageText;      
    }

    private void Update()
    {        
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        damagetext.color = alpha;
        Invoke("SetDeActive", destroyTime);               
    }
        
    void ViewDamageText(float damage)
    {
        damagetext.text = damage.ToString();
    }

    private void SetDeActive()
    {        
        gameObject.SetActive(false);                                            // Enemy 비활성화        
    }
}

