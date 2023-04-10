using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem_Box : MapItem_Base
{
    public int exp = 5;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Skill1") || collision.gameObject.CompareTag("Skill3"))
        {
            this.gameObject.SetActive(false);
            player.AddExp(exp);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Skill2"))
        {
            this.gameObject.SetActive(false);
            player.AddExp(exp);
        }

    }
}
