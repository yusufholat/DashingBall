using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemy : MonoBehaviour
{
    public ParticleSystem die;
    public int lifeCount;
    int hitCount = 0;
    float bugTime = 0;

    public Sprite brokenEnemy;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            hitCount++;
            GetComponent<SpriteRenderer>().sprite = brokenEnemy;
            if (hitCount == lifeCount) {
                Instantiate(die, collision.contacts[0].point, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BigEnemy"))
        {
            Instantiate(die, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("BlackHoleArea"))
        {
            Instantiate(die, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            bugTime += Time.deltaTime;
            if (bugTime > 1f) {
                Instantiate(die, collision.contacts[0].point, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShieldArea"))
        {
            Instantiate(die, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
