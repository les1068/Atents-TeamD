using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    int score;
    public int Score
    { get => score;
        set
        {
            score = value;
        }
    }
    void AddScore(int plus)
    {
        Score += plus;
    }
}
