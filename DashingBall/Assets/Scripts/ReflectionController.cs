using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReflectionController : MonoBehaviour
{
    public Player player;

    private Rigidbody2D rb;
    private float distance;
    private Vector2 movePos;
    private Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player.canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.IsPointerOverUIObject() ||
            player.canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
        {
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movePos.normalized * distance * player.playerSpeed;
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            movePos = Vector2.Reflect(lastVelocity, wallNormal).normalized;
            movePos *= distance;
        }

        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BigEnemy"))
        {
            UIManager.gameIsOver = true;
        }
    }
}
