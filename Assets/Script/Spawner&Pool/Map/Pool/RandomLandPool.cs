using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLandPool : MonoBehaviour
{
    
    public static TrackType TrackPicker()
    {
        float Rate = Random.Range(0.0f, 1.0f);

        TrackType result;
        if (Rate < 0.25f)
        {
            result = TrackType.Land1;
        }
        else if (Rate < 0.5f)
        {
            result = TrackType.Land2;
        }
        else if (Rate < 0.75f)
        {
            result = TrackType.Land3;
        }
        else result = TrackType.Land4;


        return result;
    }
}
