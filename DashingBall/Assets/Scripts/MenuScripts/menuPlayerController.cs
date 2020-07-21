using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPlayerController : MonoBehaviour
{
    float playerSpeed = 1.5f;
    private Rigidbody2D rb;
    private float distanceSpeed = 8f;
    private Vector2 movePos;
    private Vector2 lastVelocity;

    public ParticleSystem crashEffect;
    public ParticleSystem dashEffect;

    public static bool playerOnShop = false;

    string[] dashs = { "dash1", "dash2", "dash3", "dash4" };

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movePos = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
    }
    void Update()
    {
        if (playerOnShop)
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.IsPointerOverUIObject() ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
            {
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 4)]);
                movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 4)]);
                movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
            }
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            //FindObjectOfType<AudioManager>().Play("PlayerBounce");
            Instantiate(crashEffect, collision.contacts[0].point, Quaternion.identity);
        }
    }
}
