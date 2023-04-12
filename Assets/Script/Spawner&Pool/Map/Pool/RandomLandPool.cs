using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLandPool : MonoBehaviour
{
    
    public static LandType LandPicker()
    {
        float Rate = Random.Range(0.0f, 1.0f);

        LandType result;
        if (Rate < 0.25f)
        {
            result = LandType.Land1;
        }
        else if (Rate < 0.5f)
        {
            result = LandType.Land2;
        }
        else if (Rate < 0.75f)
        {
            result = LandType.Land3;
        }
        else result = LandType.Land4;


        return result;
    }
}
