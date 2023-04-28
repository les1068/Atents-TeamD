using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MapItem_Box : MapItem_Base
{
    public int exp = 5;
    public float starspawnRatio = 0.7f;
    public float heartspawnRatio = 0.3f;
    float r;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Skill1") || collision.gameObject.CompareTag("Skill3"))
        {
            RandomItemSpawn();
            this.gameObject.SetActive(false);
            player.AddExp(exp);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Skill2"))
        {
            RandomItemSpawn();
            this.gameObject.SetActive(false);
            player.AddExp(exp);
        }

    }

    private void RandomItemSpawn()
    {
        r = Random.value;
        if (r < heartspawnRatio)
        {
            GameObject obj = Factory.Inst.GetObject(PoolObjectType.ItemStar);
            ItemStar item = obj.GetComponent<ItemStar>();
            item.transform.position = transform.position + Vector3.up * r;
        }
        else if (r < starspawnRatio)
        {
            GameObject obj = Factory.Inst.GetObject(PoolObjectType.ItemHeart);
            ItemHeart item = obj.GetComponent<ItemHeart>();
            item.transform.position = transform.position + Vector3.left * r;
        }
    }
}
