using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    int score;
    public Action<int> onScoreChange;
    public int Score
    { get => score;
        set
        {
            score = value;
            onScoreChange?.Invoke(score);
        }
    }
    void AddScore(int plus)
    {
        Score += plus;
    }
}
