using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapItem_Mushroom_color : MapItem_Base
{
    public float HPheal = 30;
    public int exp = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            player.AddHP(HPheal);
            player.AddExp(exp);
        }

    }
    
}
