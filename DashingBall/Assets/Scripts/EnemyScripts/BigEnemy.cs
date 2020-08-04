using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float startedSpeed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Time.deltaTime * Vector2.down * startedSpeed;
        startedSpeed += speed;
    }
}
