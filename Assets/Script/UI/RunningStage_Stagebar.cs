using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RunningStage_Stagebar : MonoBehaviour
{
    Slider slider;
    PlatformKillzone killzone;
    
    private void Start()
    {
        slider = GetComponent<Slider>();
        Transform BackGround = transform.GetChild(0);
        Transform FillArea = transform.GetChild(1);
        Transform HandleSlideArea = transform.GetChild(2);
        Transform ImageGoal = transform.GetChild(3);

    }

    private void OnEnable()
    {
        killzone = FindObjectOfType<PlatformKillzone>();
        killzone.onPlatformChanged += SetValue;

    }

    void SetValue(int platform)
    {
        float ratio  = (float)(platform) / (float)(killzone.platformCountEnd);
        slider.value = ratio;
    }

}
