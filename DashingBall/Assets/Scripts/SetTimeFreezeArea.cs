using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeFreezeArea : MonoBehaviour
{
    float x, y;
    private void Awake()
    {
        
        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var middle = upperRight - lowerLeft;
        x = middle.x;
        y = middle.y;

        GetComponent<BoxCollider2D>().size = upperRight - lowerLeft + new Vector3(5, 5);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
