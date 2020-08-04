using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    Slider slider;
    public Image fill;

    public void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxLevel(float maxLevel)
    {
        slider.maxValue = maxLevel;
    }

    public void SetLevel(float level)
    {
        slider.value = level;
    }

    public void SliderNextValue()
    {
        if(slider.value < slider.maxValue)
        slider.value++;
    }
}
