using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : PoolObject
{
    Player player;
    TextMeshProUGUI damagetext;
    Color alpha;

    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;    
    float viewdamage;

    void Start()
    {
        player = FindObjectOfType<Player>();
        damagetext = GetComponentInChildren<TextMeshProUGUI>();
        alpha = damagetext.color;

        player.ondamage += ViewDamageText;

        destroyTime = 2.0f;
    }

    // Update is called once per frame
    void ViewDamageText(float damage)
    {
        float time = 0;
        while(destroyTime > time)
        {
            time += Time.deltaTime;
            damagetext.text = damage.ToString();
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
            damagetext.color = alpha;            
        }        
        SetDeActive();        
    }

    private void SetDeActive()
    {        
        gameObject.SetActive(false);                                            // Enemy 비활성화        
    }
}

