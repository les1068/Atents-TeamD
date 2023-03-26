using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrackPool : MonoBehaviour
{
    
    public static TrackType TrackPicker()
    {
        float Rate = Random.Range(0.0f, 1.0f);

        TrackType result;
        if (Rate < 0.5)
        {
            result = TrackType.Track0;
        }
        else
        {
            result = TrackType.Track1;
        }
        return result;
    }
}
