using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuReflectionController : MonoBehaviour
{
    float playerSpeed = 1.3f;
    private Rigidbody2D rb;
    private float distanceSpeed;
    private Vector2 movePos;
    private Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movePos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        distanceSpeed = 5f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        }

    }

    void FixedUpdate()
    {
        rb.velocity = movePos.normalized * distanceSpeed * playerSpeed;
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            movePos = Vector2.Reflect(lastVelocity, wallNormal).normalized;
            movePos *= distanceSpeed;
        }
    }
}
