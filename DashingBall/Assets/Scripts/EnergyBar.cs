using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    Slider slider;
    public Gradient gradient;
    Image fill;
    public void Awake() {
        slider = GetComponent<Slider>();
        fill = gameObject.GetComponentInChildren<Image>();
    } 

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
