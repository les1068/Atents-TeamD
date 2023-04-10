using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem_Base : MonoBehaviour
{
    protected Player player;

    private void OnEnable()
    {
        player= FindObjectOfType<Player>();
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            this.gameObject.SetActive(false);
        }
    }

}
