using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image icon;
    public void Awake() {
        slider = GetComponent<Slider>();
    } 

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1f);
        icon.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        icon.color = gradient.Evaluate(slider.normalizedValue);
    }
}
