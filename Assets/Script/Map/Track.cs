using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float trackmoveSpeed = 10.0f;  // 맵 트랙 속도
    void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.left * trackmoveSpeed);
    }
}
