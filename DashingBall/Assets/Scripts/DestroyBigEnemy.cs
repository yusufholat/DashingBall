using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBigEnemy : MonoBehaviour
{
    public ParticleSystem die;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Instantiate(die, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
}
