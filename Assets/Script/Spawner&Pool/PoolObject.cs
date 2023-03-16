using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public Action onDisable;
    protected virtual void OnDisable()
    {
        onDisable?.Invoke();
    }

    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
