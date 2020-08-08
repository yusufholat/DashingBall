using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEdges : MonoBehaviour
{
    float minX, maxX, minY, maxY;

    List<Vector2> newVerticies = new List<Vector2>();
    EdgeCollider2D col;

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
        newVerticies.Add(new Vector2(minX, minY));
        newVerticies.Add(new Vector2(maxX, minY));
        newVerticies.Add(new Vector2(maxX, maxY));

    }

    void Start()
    {
        col.points = newVerticies.ToArray();
    }

    private void Update()
    {
        int health = (int)PlayerManager.currentHealth;
    }
}
