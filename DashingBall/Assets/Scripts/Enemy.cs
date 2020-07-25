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

    float lerpSpeed = 12f;
    float firstSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        randomMovePos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 0f));
        firstSpeed = speed;
    }

    private void Update()
    {
        if (PlayerManager.timeFreezePower)
        {
            speed -= Time.deltaTime * lerpSpeed;
            if (speed < 0)
                speed = 0;
        }
        else
        {
            speed += Time.deltaTime * lerpSpeed;
            if (speed > firstSpeed)
                speed = firstSpeed;
        }
    }

    void FixedUpdate() {
        rb.velocity = randomMovePos.normalized * speed;
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox") || collision.gameObject.CompareTag("GameOver")){
            Vector2 wallNormal = collision.contacts[0].normal;
            randomMovePos = Vector2.Reflect(lastVelocity, wallNormal).normalized;
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Instantiate(bigEnemy, collision.contacts[0].point, Quaternion.identity);
        }
    }

}