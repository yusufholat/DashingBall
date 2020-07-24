using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem die;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 randomMovePos;
    private Vector2 lastVelocity;

    public GameObject bigEnemy;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        randomMovePos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 0f));
    }

    private void Update()
    {
        
    }

    void FixedUpdate() {
        rb.velocity = randomMovePos.normalized * speed;
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox") || collision.gameObject.CompareTag("GameOver")) {
            Vector2 wallNormal = collision.contacts[0].normal;
            randomMovePos = Vector2.Reflect(lastVelocity, wallNormal).normalized;
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Instantiate(bigEnemy, collision.contacts[0].point, Quaternion.identity);
        }
    }
}