using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Effect : PoolObject
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

     private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(LifeOver(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }
}
