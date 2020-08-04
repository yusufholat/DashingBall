using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemy : MonoBehaviour
{
    public ParticleSystem die;
    public int lifeCount = 4;
    int hitCount = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            hitCount++;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShieldArea"))
        {
            Instantiate(die, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
