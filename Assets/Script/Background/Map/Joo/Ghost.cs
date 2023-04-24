using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : TrapBase
{
    [Range(0.0f, 1.0f)]
    public float damageRatio = 0.4f;
    [Range(1.0f, 50.0f)]
    public float pushForce = 30.0f;
    public float moveSpeed = 2.0f;
    int dir;
    SpriteRenderer ghostRenderer;

    private void Start()
    {
        dir = 1;
        ghostRenderer = GetComponent<SpriteRenderer>();
        ghostRenderer.flipX = false;
    }
    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * dir * Vector2.left, Space.Self);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        dir *= -1;
        if(dir <0)
        { 
        ghostRenderer.flipX = !true;
        }
        else { ghostRenderer.flipX = true; }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player obj = collision.gameObject.GetComponent<Player>();
            if (obj != null)
            {
                obj.HP -= obj.maxHp * damageRatio;
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
            }
        }
    }

}
