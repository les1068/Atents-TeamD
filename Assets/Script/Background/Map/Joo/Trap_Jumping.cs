using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Jumping : TrapBase
{
    public float force = 40.0f;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player obj = collision.gameObject.GetComponent<Player>();
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(3,0.5f) * force, ForceMode2D.Impulse);
        }
    }
}

