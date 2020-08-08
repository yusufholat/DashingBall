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

    List<Vector3> linePoints = new List<Vector3>();
    LineRenderer lineRenderer;
    float minX, maxX, minY, maxY;

    public void Awake() {
        slider = GetComponent<Slider>();
        lineRenderer = GetComponent<LineRenderer>();

        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        maxY = upperRight.y;
        minY = lowerLeft.y;

        linePoints.Add(new Vector2(minX, maxY * 2)); //*2 for particle material strecth mode 
        linePoints.Add(new Vector2(minX, minY));
        linePoints.Add(new Vector2(maxX, minY));
        linePoints.Add(new Vector2(maxX, maxY * 2)); //*2 for particle material strecth mode 
    }

    private void Start()
    {
        lineRenderer.SetPositions(linePoints.ToArray());
    }

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1f);
        icon.color = gradient.Evaluate(1f);
        lineRenderer.startColor = gradient.Evaluate(1f);
        lineRenderer.endColor = gradient.Evaluate(1f);
    }
    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        icon.color = gradient.Evaluate(slider.normalizedValue);
        lineRenderer.startColor = gradient.Evaluate(slider.normalizedValue);
        lineRenderer.endColor = gradient.Evaluate(slider.normalizedValue);
    }
}
