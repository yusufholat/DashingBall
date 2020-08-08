using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPlayerReflection : MonoBehaviour
{
    TutorialPlayer player;

    Rigidbody2D rb;
    float distanceSpeed;
    Vector2 movePos;
    Vector2 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<TutorialPlayer>();
    }


    void Update()
    {
        if (player.canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !IsPointerOverUIObject() && TutorialManager.tutorialStarted ||
        player.canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !IsPointerOverUIObject() && TutorialManager.tutorialStarted)
        {
            movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            distanceSpeed = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
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

        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BigEnemy"))
        {
            TutorialManager.instance.RestartPlayer();
        }
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
