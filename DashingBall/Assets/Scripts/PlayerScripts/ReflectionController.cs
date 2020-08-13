using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReflectionController : MonoBehaviour
{
    Player player;

    Rigidbody2D rb;
    float distanceSpeed;
    Vector2 movePos;
    Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        if (!GameManager.gameStarded)
        {
            movePos = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
            distanceSpeed = 8f;
        }
    }

    void Update()
    {
        if (!GameManager.gameStarded)
        {
            if (menuUIManager.shopIsOpen)
            {
                if (Input.GetMouseButtonDown(0) && !GameManager.IsPointerOverUIObject() ||
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
                {
                    movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) ||
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
                }
            }
        }
        else
        {
            if (player.canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.IsPointerOverUIObject() ||
            player.canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
            {
                movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
            }
        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = movePos.normalized * distanceSpeed * 1.5f;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShopOutsideArea"))
        {
            movePos = new Vector3(3, 3, 0) - transform.position;
            distanceSpeed = 10f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShopOutsideArea"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.tag = "Hitbox";
        }
    }
}
