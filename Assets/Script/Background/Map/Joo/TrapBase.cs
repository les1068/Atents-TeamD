using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase: MonoBehaviour
{
    float damage=2.0f;
    protected float Damage
    { get => damage;
        set { damage = value; }
    }
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void OnEnable()
    {
        Damage = damage ;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player obj = collision.gameObject.GetComponent<Player>();
            obj.HP -= damage;
            obj.OnInvincibleMode();
            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 16.0f, ForceMode2D.Impulse);
        }
    }


}
