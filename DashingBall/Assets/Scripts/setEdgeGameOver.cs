using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setEdgeGameOver : MonoBehaviour
{
    float minX, maxX, minY, maxY;

    List<Vector2> newVerticies = new List<Vector2>();
    EdgeCollider2D col;
    float offset = 5;

    private void Awake()
    {
        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        maxY = upperRight.y;
        minY = lowerLeft.y;

        col = GetComponent<EdgeCollider2D>();
        newVerticies.Add(new Vector2(minX, maxY));
        newVerticies.Add(new Vector2(minX, maxY + offset));
        newVerticies.Add(new Vector2(maxX, maxY + offset));
        newVerticies.Add(new Vector2(maxX, maxY));
    }

    void Start()
    {
        col.points = newVerticies.ToArray();
    }
}
